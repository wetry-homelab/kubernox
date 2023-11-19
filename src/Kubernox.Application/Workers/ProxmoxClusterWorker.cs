using Kubernox.Application.Interfaces;
using Kubernox.Infrastructure.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kubernox.Application.Workers
{
    public class ProxmoxClusterWorker : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public ProxmoxClusterWorker(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    var proxmoxClient = scope.ServiceProvider.GetRequiredService<IProxmoxClient>();
                    var clusterConfigurationRepository = scope.ServiceProvider.GetRequiredService<IHostConfigurationRepository>();
                    var nodeRepository = scope.ServiceProvider.GetRequiredService<INodeRepository>();

                    var clusters = await clusterConfigurationRepository.GetHostsAsync();
                    var nodes = await nodeRepository.GetNodesAsync();

                    foreach (var cluster in clusters)
                    {
                        var clusterNodes = await proxmoxClient.GetNodesAsync(cluster.Ip, cluster.ApiToken);

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
                                    CreateBy = typeof(ProxmoxClusterWorker).Name
                                });
                            }
                            else
                            {
                                nodeExisting.UpdateBy = typeof(ProxmoxClusterWorker).Name;
                                nodeExisting.UpdateAt = DateTime.UtcNow;
                                nodeExisting.MaxMemory = clusterNode.MemorySize;
                                nodeExisting.MaxCpu = clusterNode.CpuSize;

                                await nodeRepository.UpdateNodeAsync(nodeExisting);
                            }
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(5));
            }
        }
    }
}
