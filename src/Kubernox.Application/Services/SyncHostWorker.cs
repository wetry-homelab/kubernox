using Kubernox.Application.Interfaces;
using Kubernox.Infrastructure.Interfaces;

using Microsoft.Extensions.Logging;

namespace Kubernox.Application.Services
{
    public class SyncHostWorker : ISyncHostWorker
    {
        private readonly ILogger<SyncHostWorker> logger;
        private readonly IProxmoxClient proxmoxClient;
        private readonly IHostConfigurationRepository hostConfigurationRepository;
        private readonly INodeRepository nodeRepository;

        public SyncHostWorker(ILogger<SyncHostWorker> logger, IProxmoxClient proxmoxClient, IHostConfigurationRepository hostConfigurationRepository, INodeRepository nodeRepository)
        {
            this.logger = logger;
            this.proxmoxClient = proxmoxClient;
            this.hostConfigurationRepository = hostConfigurationRepository;
            this.nodeRepository = nodeRepository;
        }

        public async Task ProcessHostAsync()
        {
            var clusters = await hostConfigurationRepository.GetHostsAsync();
            var nodes = await nodeRepository.GetNodesAsync();

            foreach (var cluster in clusters)
            {
                try
                {
                    var clusterNodes = await proxmoxClient.GetNodesAsync(cluster.Ip, cluster.ApiToken);
                    cluster.Status = "Online";

                    foreach (var clusterNode in clusterNodes)
                    {
                        var nodeExisting = nodes.FirstOrDefault(f => f.NodeId == clusterNode.Id);

                        if (nodeExisting == null)
                        {
                            await nodeRepository.AddNodeAsync(new Domain.Entities.Node()
                            {
                                NodeId = clusterNode.Id,
                                Name = clusterNode.Id,
                                HostConfigurationId = cluster.Id,
                                MaxCpu = clusterNode.CpuSize,
                                MaxMemory = clusterNode.MemorySize,
                                Id = Guid.NewGuid(),
                                CreateAt = DateTime.UtcNow,
                                CreateBy = typeof(SyncHostWorker).Name
                            });
                        }
                        else
                        {
                            nodeExisting.UpdateBy = typeof(SyncHostWorker).Name;
                            nodeExisting.UpdateAt = DateTime.UtcNow;
                            nodeExisting.MaxMemory = clusterNode.MemorySize;
                            nodeExisting.MaxCpu = clusterNode.CpuSize;

                            await nodeRepository.UpdateNodeAsync(nodeExisting);
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Error on ExecuteAsync");
                    cluster.Status = "Offline";
                }

                await hostConfigurationRepository.UpdateHostConfigurationAsync(cluster);
            }
        }
    }
}
