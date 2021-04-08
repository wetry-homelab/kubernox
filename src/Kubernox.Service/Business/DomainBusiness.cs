using Application.Entities;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using DnsClient;
using DnsClient.Protocol;
using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.Service.Business
{
    public class DomainBusiness : IDomainBusiness
    {
        private readonly IDomainRepository domainRepository;
        private readonly IClusterDomainRepository clusterDomainRepository;
        private readonly IClusterRepository clusterRepository;
        private readonly IMapper mapper;
        private readonly ILookupClient lookupClient;
        private readonly ITraefikRouterService traefikRouterService;
        private readonly ITraefikRedisStore traefikCache;
        private readonly ILogger<DomainBusiness> logger;

        private static Random random = new Random();

        public DomainBusiness(IDomainRepository domainNameRepository, IMapper mapper, ILookupClient lookupClient, ITraefikRedisStore traefikCache, IClusterRepository clusterRepository, IClusterDomainRepository clusterDomainRepository, ITraefikRouterService traefikRouterService, ILogger<DomainBusiness> logger)
        {
            if (domainNameRepository == null)
                throw new ArgumentNullException(nameof(domainNameRepository));

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            if (lookupClient == null)
                throw new ArgumentNullException(nameof(lookupClient));

            if (traefikCache == null)
                throw new ArgumentNullException(nameof(traefikCache));

            if (clusterRepository == null)
                throw new ArgumentNullException(nameof(clusterRepository));

            this.domainRepository = domainNameRepository;
            this.mapper = mapper;
            this.lookupClient = lookupClient;
            this.traefikCache = traefikCache;
            this.clusterRepository = clusterRepository;
            this.clusterDomainRepository = clusterDomainRepository;
            this.traefikRouterService = traefikRouterService;
            this.logger = logger;
        }

        public async Task<DomainItemResponse[]> ListDomainsAsync()
        {
            var domains = await domainRepository.ReadsAsync(d => d.DeleteAt == null);
            return domains.Select(d => mapper.Map<DomainItemResponse>(d)).OrderBy(d => d.RootDomain).ToArray();
        }

        public async Task<ClusterDomainItemResponse[]> ListDomainsForClusterAsync(string clusterId)
        {
            var domains = await clusterDomainRepository.ReadsAsync(d => d.ClusterId == clusterId);
            return domains.Select(d => mapper.Map<ClusterDomainItemResponse>(d)).OrderBy(d => d.Value).ToArray();
        }

        public async Task<bool> CreateDomainAsync(DomainNameCreateRequest request)
        {
            var domainExist = await domainRepository.ReadAsync(d => d.Value == request.RootDomain);

            if (domainExist != null)
                throw new DuplicateException();

            var domainNameToCreate = mapper.Map<Domain>(request);
            domainNameToCreate.Value = request.RootDomain;
            domainNameToCreate.ValidationKey = GenerateChallenge(48);

            var insertResult = await domainRepository.InsertAsync(domainNameToCreate);

            return insertResult > 0;
        }

        public async Task<bool> ValidateDomainAsync(string id)
        {
            var domainName = await domainRepository.ReadAsync(d => d.Id == id);

            if (domainName == null)
                throw new NotFoundException();

            var result = lookupClient.Query($"{domainName.Id}.{domainName.Value}", QueryType.TXT);

            if (result.Answers.OfType<TxtRecord>().Any(record => record.Text.Any(a => a == domainName.ValidationKey)))
            {
                domainName.ValidationDate = DateTime.UtcNow;

                return (await domainRepository.UpdateAsync(domainName) > 0);
            }

            return false;
        }

        public async Task<bool> DeleteDomainAsync(string id)
        {
            var domainName = await domainRepository.ReadAsync(d => d.Id == id);

            if (domainName == null)
                throw new NotFoundException();

            var subDomains = await clusterDomainRepository.ReadsAsync(d => d.RootDomainId == id);

            if (await domainRepository.DeleteAsync(domainName) > 0)
            {
                return await clusterDomainRepository.DeletesAsync(subDomains) == subDomains.Length;
            }

            return false;
        }


        public async Task<bool> LinkDomainToClusterAsync(DomainLinkingRequestContract request)
        {
            var domainName = await domainRepository.ReadAsync(d => d.Id == request.DomainId && d.ValidationDate.HasValue);
            var cluster = await clusterRepository.ReadAsync(c => c.Id == request.ClusterId && !c.DeleteAt.HasValue);
            var domainLink = await domainRepository.ReadAsync(d => d.Value == request.SubDomain);

            if (domainLink != null)
                throw new DuplicateException();

            if (domainName == null || cluster == null)
                throw new NotFoundException();

            if (domainName.ValidationDate.HasValue)
            {
                try
                {
                    if (request.Protocol == "HTTP")
                    {
                        await ProcessHttpDomainLink(request, cluster, domainName);
                    }
                    else if (request.Protocol == "TLS")
                    {
                        await ProcessTlsDomainLink(request, cluster, domainName);
                    }
                    else
                    {
                        await ProcessHttpsDomainLink(request, cluster, domainName);
                    }

                    return true;
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Error on linking.");
                }
            }

            return false;
        }

        private Task ProcessTlsDomainLink(DomainLinkingRequestContract request, Cluster cluster, Domain domainName)
        {
            throw new NotImplementedException();
        }

        private async Task ProcessHttpDomainLink(DomainLinkingRequestContract request, Cluster cluster, Domain domainName)
        {
            var linkEntry = new ClusterDomain()
            {
                ClusterId = cluster.Id,
                Name = string.IsNullOrEmpty(request.SubDomain) ? domainName.Value : request.SubDomain,
                Value = string.IsNullOrEmpty(request.SubDomain) ? domainName.Value : request.SubDomain,
                RootDomainId = domainName.Id,
                Protocol = request.Protocol,
                Resolver = "none"
            };

            if ((await clusterDomainRepository.InsertAsync(linkEntry)) > 0)
            {
                var domainToInsert = string.IsNullOrEmpty(request.SubDomain) ? domainName.Value : $"{request.SubDomain}.{domainName.Value}";
                await traefikRouterService.StoreNewHttpRule(cluster, domainToInsert);
            }
        }

        private async Task ProcessHttpsDomainLink(DomainLinkingRequestContract request, Cluster cluster, Domain domainName)
        {
            var linkEntry = new ClusterDomain()
            {
                ClusterId = cluster.Id,
                Name = string.IsNullOrEmpty(request.SubDomain) ? domainName.Value : request.SubDomain,
                Value = string.IsNullOrEmpty(request.SubDomain) ? domainName.Value : request.SubDomain,
                RootDomainId = domainName.Id,
                Protocol = request.Protocol,
                Resolver = request.Resolver
            };

            if ((await clusterDomainRepository.InsertAsync(linkEntry)) > 0)
            {
                var domainToInsert = string.IsNullOrEmpty(request.SubDomain) ? domainName.Value : $"{request.SubDomain}.{domainName.Value}";
                await traefikRouterService.StoreNewHttpsRule(cluster, request.SubDomain, domainName.Value);
            }
        }

        private async Task RefreshClusterCacheRouting()
        {

        }

        private async Task LinkDomainToClusterCacheAsync(string clusterName, string ip, string domain, string resolver)
        {
            var ruleName = domain.Replace(".", "-");

            var values = new List<KeyValuePair<string, string>>();

            values.Add(new KeyValuePair<string, string>($"traefik/http/services/{clusterName}-https-{ruleName}-lb/loadbalancer/servers/0/url", $"https://{ip}:443"));
            values.Add(new KeyValuePair<string, string>($"traefik/http/routers/{clusterName}-https-{ruleName}/entrypoints/0", $"websecure"));
            values.Add(new KeyValuePair<string, string>($"traefik/http/routers/{clusterName}-https-{ruleName}/tls", "true"));
            values.Add(new KeyValuePair<string, string>($"traefik/http/routers/{clusterName}-https-{ruleName}/service", $"{clusterName}-https-{ruleName}-lb"));
            values.Add(new KeyValuePair<string, string>($"traefik/http/routers/{clusterName}-https-{ruleName}/rule", $"Host(`{domain}`)"));
            values.Add(new KeyValuePair<string, string>($"traefik/http/routers/{clusterName}-https-{ruleName}/tls/domains/0/main", $"{domain}"));

            if (!string.IsNullOrEmpty(resolver))
                values.Add(new KeyValuePair<string, string>($"traefik/http/routers/{clusterName}-https-{ruleName}/tls/certresolver", $"{resolver}"));

            await traefikCache.StoreValues(values);
        }

        public static string GenerateChallenge(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
