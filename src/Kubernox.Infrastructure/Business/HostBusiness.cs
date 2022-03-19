using Kubernox.Infrastructure.Contracts.Request;
using Kubernox.Infrastructure.Contracts.Response;
using Kubernox.Infrastructure.Core.Exceptions;
using Kubernox.Infrastructure.Infrastructure.Entities;
using Kubernox.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.Infrastructure.Business
{
    public class HostBusiness : IHostBusiness
    {
        private readonly ILogger<HostBusiness> logger;
        private readonly IHostRepository hostRepository;

        public HostBusiness(ILogger<HostBusiness> logger, IHostRepository hostRepository)
        {
            this.logger = logger;
            this.hostRepository = hostRepository;
        }

        public async Task<HostListItemResponse[]> GetHostListAsync()
        {
            var hosts = await hostRepository.ReadAllHostAsync();

            return hosts.Select(s => new HostListItemResponse()
            {
                Id = s.Id,
                CreateAt = s.CreateAt,
                Ip = s.Ip,
                Name = s.Name,
                User = s.User
            }).ToArray();
        }

        public async Task<Guid?> CreateHostAsync(HostCreateRequest request)
        {
            var hosts = await hostRepository.ReadAllHostAsync();

            if(hosts.Any(a => a.Ip == request.Ip))
            {
                throw new DuplicateException($"Host with IP {request.Ip} already exist.");
            }

            var hostDb = new Host()
            {
                Id = Guid.NewGuid(),
                Ip = request.Ip,
                CreateAt = DateTime.UtcNow,
                Name = request.Name,
                Token = request.Token,
                User = request.User
            };

            return await hostRepository.InsertHostAsync(hostDb);
        }
    }
}
