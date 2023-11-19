using Kubernox.Infrastructure.Interfaces;
using Kubernox.Shared.Contracts.Response;

using MediatR;

namespace Kubernox.Application.Features.Host.Queries
{
    public class GetHostQueryHandler : IRequestHandler<GetHostQuery, IEnumerable<HostItemResponse>>
    {
        private readonly IHostConfigurationRepository hostConfigurationRepository;
        private readonly INodeRepository nodeRepository;

        public GetHostQueryHandler(IHostConfigurationRepository hostConfigurationRepository, INodeRepository nodeRepository)
        {
            this.hostConfigurationRepository = hostConfigurationRepository;
            this.nodeRepository = nodeRepository;
        }

        public async Task<IEnumerable<HostItemResponse>> Handle(GetHostQuery request, CancellationToken cancellationToken)
        {
            var hostConfigurations = await hostConfigurationRepository.GetHostsAsync();
            var nodes = await nodeRepository.GetNodesAsync();

            return hostConfigurations.Select(s => new HostItemResponse()
            {
                Id = s.Id,
                Ip = s.Ip,
                IsActive = s.IsActive,
                Name = s.Name,
                NodeCount = nodes.Count(c => c.HostConfigurationId == s.Id),
                Nodes = nodes.Where(c => c.HostConfigurationId == s.Id).Select(n => new NodeItemResponse()
                {
                    Id = n.Id,
                    Name = n.Name
                }).ToList()
            }).ToList();
        }
    }
}
