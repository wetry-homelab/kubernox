using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using DnsClient;
using DnsClient.Protocol;
using Domain.Entities;
using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.Service.Business
{
    public class DomaineNameBusiness : IDomaineNameBusiness
    {
        private readonly IDomainNameRepository domainNameRepository;
        private readonly IMapper mapper;
        private readonly ILookupClient lookupClient;
        private static Random random = new Random();

        public DomaineNameBusiness(IDomainNameRepository domainNameRepository, IMapper mapper, ILookupClient lookupClient)
        {
            if (domainNameRepository == null)
                throw new ArgumentNullException(nameof(domainNameRepository));

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            if (lookupClient == null)
                throw new ArgumentNullException(nameof(lookupClient));

            this.domainNameRepository = domainNameRepository;
            this.mapper = mapper;
            this.lookupClient = lookupClient;
        }

        public async Task<DomainNameItemResponse[]> ListDomainsAsync()
        {
            var domains = await domainNameRepository.ReadsAsync();
            return domains.Select(d => mapper.Map<DomainNameItemResponse>(d)).ToArray();
        }

        public async Task<bool> CreateDomainAsync(DomainNameCreateRequest request)
        {
            var domainExist = await domainNameRepository.ReadAsync(d => d.RootDomain == request.RootDomain);

            if (domainExist != null)
                throw new DuplicateException();

            var domainNameToCreate = mapper.Map<DomainName>(request);
            domainNameToCreate.ValidationKey = GenerateChallenge(48);

            var insertResult = await domainNameRepository.InsertAsync(domainNameToCreate);

            return insertResult > 0;
        }

        public async Task<bool> ValidateDomainAsync(string id)
        {
            var domainName = await domainNameRepository.ReadAsync(d => d.Id == id);

            if (domainName == null)
                throw new NotFoundException();

            var result = lookupClient.Query(domainName.RootDomain, QueryType.TXT);

            if (result.Answers.OfType<TxtRecord>().Any(record => record.Text.ToString() == domainName.ValidationKey))
            {
                domainName.ValidationDate = DateTime.UtcNow;

                return (await domainNameRepository.UpdateAsync(domainName) > 0);
            }

            return false;
        }

        public static string GenerateChallenge(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
