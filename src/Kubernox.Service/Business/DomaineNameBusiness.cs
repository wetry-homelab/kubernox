using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using DnsClient;
using DnsClient.Protocol;
using Domain.Entities;
using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.Service.Business
{
    public class DomaineNameBusiness : IDomaineNameBusiness
    {
        private readonly IDomainNameRepository domainNameRepository;
        private readonly IClusterRepository clusterRepository;
        private readonly IMapper mapper;
        private readonly ILookupClient lookupClient;
        private readonly ITraefikRedisStore traefikCache;

        private static Random random = new Random();

        public DomaineNameBusiness(IDomainNameRepository domainNameRepository, IMapper mapper, ILookupClient lookupClient, ITraefikRedisStore traefikCache, IClusterRepository clusterRepository)
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

            this.domainNameRepository = domainNameRepository;
            this.mapper = mapper;
            this.lookupClient = lookupClient;
            this.traefikCache = traefikCache;
            this.clusterRepository = clusterRepository;
        }

        public async Task<DomainNameItemResponse[]> ListDomainsAsync()
        {
            var domains = await domainNameRepository.ReadsAsync();
            return domains.Select(d => mapper.Map<DomainNameItemResponse>(d)).OrderBy(d => d.RootDomain).ToArray();
        }

        public async Task<bool> CreateDomainAsync(DomainNameCreateRequest request)
        {
            var domainExist = await domainNameRepository.ReadAsync(d => d.Value == request.RootDomain);

            if (domainExist != null)
                throw new DuplicateException();

            var domainNameToCreate = mapper.Map<DomainName>(request);
            domainNameToCreate.Value = request.RootDomain;
            domainNameToCreate.ValidationKey = GenerateChallenge(48);

            var insertResult = await domainNameRepository.InsertAsync(domainNameToCreate);

            return insertResult > 0;
        }

        public async Task<bool> ValidateDomainAsync(string id)
        {
            var domainName = await domainNameRepository.ReadAsync(d => d.Id == id);

            if (domainName == null)
                throw new NotFoundException();

            var result = lookupClient.Query($"{domainName.Id}.{domainName.Value}", QueryType.TXT);

            if (result.Answers.OfType<TxtRecord>().Any(record => record.Text.Any(a => a == domainName.ValidationKey)))
            {
                domainName.ValidationDate = DateTime.UtcNow;

                return (await domainNameRepository.UpdateAsync(domainName) > 0);
            }

            return false;
        }

        public async Task<bool> DeleteDomainAsync(string id)
        {
            var domainName = await domainNameRepository.ReadAsync(d => d.Id == id);

            if (domainName == null)
                throw new NotFoundException();

            var subDomains = await domainNameRepository.ReadsAsync(d => d.RootDomainId == id);

            if (await domainNameRepository.DeleteAsync(domainName) > 0)
            {
                return await domainNameRepository.DeletesAsync(subDomains) == subDomains.Length;
            }

            return false;
        }


        public async Task<bool> LinkDomainToClusterAsync(string domainId, DomainLinkingRequestContract request)
        {
            var domainName = await domainNameRepository.ReadAsync(d => d.Id == domainId && d.ValidationDate.HasValue);
            var cluster = await clusterRepository.ReadAsync(c => c.Id == request.ClusterId && !c.DeleteAt.HasValue);

            if (domainName == null || cluster == null)
                throw new NotFoundException();

            if (domainName.ValidationDate.HasValue)
            {
                if (request.LinkRoot)
                {
                    domainName.ClusterId = cluster.Id;
                }
                else if (!string.IsNullOrEmpty(request.Resolver))
                {

                }
                else
                {

                }
            }

            return false;
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
