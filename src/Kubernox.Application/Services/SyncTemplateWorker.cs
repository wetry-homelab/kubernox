using Kubernox.Application.Interfaces;
using Kubernox.Infrastructure.Interfaces;

using Microsoft.Extensions.Logging;

namespace Kubernox.Application.Services
{
    public class SyncTemplateWorker : ISyncTemplateWorker
    {
        private readonly ILogger<SyncTemplateWorker> logger;
        private readonly ITemplateRepository templateRepository;
        private readonly IHostConfigurationRepository hostConfigurationRepository;
        private readonly IProxmoxClient proxmoxClient;
        private readonly INodeRepository nodeRepository;

        public SyncTemplateWorker(ILogger<SyncTemplateWorker> logger, ITemplateRepository templateRepository, IHostConfigurationRepository hostConfigurationRepository, IProxmoxClient proxmoxClient, INodeRepository nodeRepository)
        {
            this.logger = logger;
            this.templateRepository = templateRepository;
            this.hostConfigurationRepository = hostConfigurationRepository;
            this.proxmoxClient = proxmoxClient;
            this.nodeRepository = nodeRepository;
        }

        public async Task ProcessTemplateAsync()
        {
            var clusters = await hostConfigurationRepository.GetHostsAsync();
            var nodes = await nodeRepository.GetNodesAsync();

            foreach (var cluster in clusters)
            {
                try
                {
                    var vms = await proxmoxClient.GetVmsAsync(cluster.Ip, cluster.ApiToken);
                    var templates = vms.Where(vm => vm.IsTemplate).ToList();
                    var templatesDb = await templateRepository.GetTemplatesAsync();

                    foreach (var template in templates)
                    {
                        var templateDb = templatesDb.FirstOrDefault(f => f.VmId == template.VmId && f.HostConfigurationId == cluster.Id);

                        var node = nodes.FirstOrDefault(f => f.HostConfigurationId == cluster.Id && f.Name == $"node/{template.Node}");

                        if (templateDb == null)
                        {
                            templateDb = new Domain.Entities.Template()
                            {
                                Id = Guid.NewGuid(),
                                Description = template.Description,
                                HostConfigurationId = cluster.Id,
                                Name = template.Name,
                                VmId = template.VmId,
                                NodeId = node?.Id ?? Guid.Empty
                            };

                            await templateRepository.AddTemplateAsync(templateDb);
                        }
                        else
                        {
                            templateDb.Description = template.Description;
                            templateDb.Name = template.Name;
                            templateDb.NodeId = node?.Id ?? Guid.Empty;

                            await templateRepository.UpdateTemplateAsync(templateDb);
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Error on ExecuteAsync");
                }
            }
        }
    }
}
