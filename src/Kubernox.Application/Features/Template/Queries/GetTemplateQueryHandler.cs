using Kubernox.Infrastructure.Interfaces;
using Kubernox.Shared;

using MediatR;

using Microsoft.Extensions.Caching.Memory;

namespace Kubernox.Application.Features.Template.Queries
{
    public class GetTemplateQueryHandler : IRequestHandler<GetTemplateQuery, IEnumerable<TemplateItemResponse>>
    {
        private readonly ITemplateRepository templateRepository;
        private readonly INodeRepository nodeRepository;
        private readonly IHostConfigurationRepository hostConfigurationRepository;
        private readonly IMemoryCache memoryCache;

        private const string TemplatesCacheKey = "templatesList";

        public GetTemplateQueryHandler(ITemplateRepository templateRepository, INodeRepository nodeRepository, IHostConfigurationRepository hostConfigurationRepository, IMemoryCache memoryCache)
        {
            this.templateRepository = templateRepository;
            this.nodeRepository = nodeRepository;
            this.hostConfigurationRepository = hostConfigurationRepository;
            this.memoryCache = memoryCache;
        }

        public async Task<IEnumerable<TemplateItemResponse>> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
        {
            if (memoryCache.TryGetValue<IEnumerable<TemplateItemResponse>>(TemplatesCacheKey, out var templatesResponse))
                return templatesResponse;

            var templates = await templateRepository.GetTemplatesAsync();
            var nodes = await nodeRepository.GetNodesAsync();
            var hosts = await hostConfigurationRepository.GetHostsAsync();

            templatesResponse = templates.Select(s => new TemplateItemResponse()
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                HostId = s.HostConfigurationId,
                HostName = hosts.FirstOrDefault(f => f.Id == s.HostConfigurationId)?.Name,
                NodeId = s.NodeId,
                NodeName = nodes.FirstOrDefault(f => f.Id == s.NodeId)?.Name
            }).ToList();

            memoryCache.Set(TemplatesCacheKey, templatesResponse);

            return templatesResponse;

        }
    }
}
