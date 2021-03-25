using Application.Interfaces;
using Application.Messages;
using Application.Entities;
using Kubernox.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ProxmoxVEAPI.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kubernox.Service.Business
{
    public class QueueBusiness : IQueueBusiness
    {
        private readonly IQueueService queueService;
        private readonly IClusterRepository clusterRepository;
        private readonly IDatacenterRepository datacenterRepository;
        private readonly IHubContext<AppHub, IAppHub> hubContext;
        private readonly ILogger<QueueBusiness> logger;

        public QueueBusiness(IQueueService queueService, IHubContext<AppHub, IAppHub> hubContext, IClusterRepository clusterRepository, ILogger<QueueBusiness> logger, IDatacenterRepository datacenterRepository)
        {
            if (queueService == null)
                throw new ArgumentNullException(nameof(queueService));

            if (clusterRepository == null)
                throw new ArgumentNullException(nameof(clusterRepository));

            if (datacenterRepository == null)
                throw new ArgumentNullException(nameof(datacenterRepository));

            this.queueService = queueService;
            this.hubContext = hubContext;
            this.clusterRepository = clusterRepository;
            this.logger = logger;
            this.datacenterRepository = datacenterRepository;
        }

        public async Task StartQueueListener()
        {
            await queueService.OnQueueMessageInit(async (message) =>
            {
                logger.LogInformation(message);

                var clusterResult = JsonSerializer.Deserialize<ClusterCreationResultMessage>(message);
                var cluster = await clusterRepository.ReadAsync(c => c.OrderId == clusterResult.Data.OrderId);

                if (cluster != null && clusterResult.Pattern == "success")
                {
                    cluster.KubeConfig = clusterResult.Data.KubeConfig.Replace($"server: https://{cluster.Ip}:6443", $"server: https://{cluster.Name}.{cluster.Domain}:6443");
                    cluster.KubeConfigJson = clusterResult.Data.KubeConfigAsJson;

                    if ((await clusterRepository.UpdateAsync(cluster)) > 0)
                    {
                        await hubContext.Clients.All.NotificationReceived("Cluster ready", "Cluster created and ready to use.", "success");
                    }
                }
            });

            await queueService.OnQueueDeleteMessageInit(async (message) =>
            {
                logger.LogInformation(message);

                var cluster = JsonSerializer.Deserialize<Cluster>(message);
                await StartCleanClusterQueueListener(cluster);
            });
        }

        public async Task StartCleanClusterQueueListener(Cluster cluster)
        {
            var qemuClient = new QemuClient();

            var statusStopped = false;
            var retry = 0;
            var node = await datacenterRepository.ReadAsync(d => d.Id == cluster.ProxmoxNodeId);
            var taskStop = new List<Task<bool>>();
            var taskDelete = new List<Task<bool>>();

            if (node != null)
            {
                do
                {
                    retry += 1;
                    taskStop.Add(qemuClient.StopQemu(node.Name, cluster.OrderId));
                    taskDelete.Add(qemuClient.DeleteQemu(node.Name, cluster.OrderId));

                    foreach (var nodeDelete in cluster.Nodes)
                    {
                        taskStop.Add(qemuClient.StopQemu(node.Name, nodeDelete.OrderId));
                        taskDelete.Add(qemuClient.DeleteQemu(node.Name, nodeDelete.OrderId));
                    }


                    if ((await Task.WhenAll(taskStop)).All(a => a))
                    {
                        await Task.Delay(10000);
                        statusStopped = (await Task.WhenAll(taskDelete)).All(a => a);
                    }

                } while (!statusStopped && retry < 10);
            }
        }
    }
}
