using Kubernox.Infrastructure.Interfaces;
using Kubernox.Shared;

using MediatR;

using Microsoft.Extensions.Caching.Memory;

namespace Kubernox.Application.Features.Host.Queries
{
    public class GetHostQueryHandler : IRequestHandler<GetHostQuery, IEnumerable<HostItemResponse>>
    {
        private readonly IHostConfigurationRepository hostConfigurationRepository;
        private readonly INodeRepository nodeRepository;
        private readonly IMemoryCache memoryCache;

        private const string HostCacheKey = "hostsList";

        public GetHostQueryHandler(IHostConfigurationRepository hostConfigurationRepository, INodeRepository nodeRepository, IMemoryCache memoryCache)
        {
            this.hostConfigurationRepository = hostConfigurationRepository;
            this.nodeRepository = nodeRepository;
            this.memoryCache = memoryCache;
        }

        public async Task<IEnumerable<HostItemResponse>> Handle(GetHostQuery request, CancellationToken cancellationToken)
        {
            if (memoryCache.TryGetValue<IEnumerable<HostItemResponse>>(HostCacheKey, out var hosts))
                return hosts;

            var hostConfigurations = await hostConfigurationRepository.GetHostsAsync();
            var nodes = await nodeRepository.GetNodesAsync();

            hosts = hostConfigurations.Select(s => new HostItemResponse()
            {
                Id = s.Id,
                Ip = s.Ip,
                Status = s.Status,
                IsActive = s.IsActive,
                Name = s.Name,
                NodeCount = nodes.Count(c => c.HostConfigurationId == s.Id),
                Nodes = nodes.Where(c => c.HostConfigurationId == s.Id).Select(n => new NodeItemResponse()
                {
                    Id = n.Id,
                    Name = n.Name
                }).ToList()
            }).ToList();

            memoryCache.Set(HostCacheKey, hosts);

            return hosts;
        }
    }
}
