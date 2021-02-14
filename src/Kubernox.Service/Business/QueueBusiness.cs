using Application.Interfaces;
using Application.Messages;
using Kubernox.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kubernox.Service.Business
{
    public class QueueBusiness : IQueueBusiness
    {
        private readonly IQueueService queueService;
        private readonly IClusterRepository clusterRepository;
        private readonly IHubContext<AppHub, IAppHub> hubContext;
        private readonly ILogger<QueueBusiness> logger;

        public QueueBusiness(IQueueService queueService, IHubContext<AppHub, IAppHub> hubContext, IClusterRepository clusterRepository, ILogger<QueueBusiness> logger)
        {
            this.queueService = queueService;
            this.hubContext = hubContext;
            this.clusterRepository = clusterRepository;
            this.logger = logger;
        }

        public async Task StartQueueListener()
        {
            await queueService.OnQueueMessageInit(async (message) =>
            {
                logger.LogDebug(message);

                var clusterResult = JsonSerializer.Deserialize<ClusterCreationResultMessage>(message);
                var cluster = await clusterRepository.ReadAsync(c => c.OrderId == clusterResult.Data.OrderId);

                if (cluster != null)
                {
                    cluster.KubeConfig = clusterResult.Data.KubeConfig.Replace($"server: https://{cluster.Ip}:6443", $"server: https://k3s-master-{cluster.Name}.{cluster.Domain}:6443");
                    cluster.KubeConfigJson = clusterResult.Data.KubeConfigAsJson;

                    if ((await clusterRepository.UpdateClusterAsync(cluster)) > 0)
                    {
                        await hubContext.Clients.All.NotificationReceived("Cluster ready", "Cluster created and ready to use.", "success");
                    }
                }
            });
        }
    }
}
