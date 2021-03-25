using Application.Interfaces;
using Application.Entities;
using k8s;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Kubernox.Workers.Business
{
    public class ClusterMonitoringBusiness : IClusterMonitoringBusiness
    {
        private readonly IClusterRepository clusterRepository;
        private readonly IClusterNodeRepository clusterNodeRepository;
        private readonly IMetricRepository metricRepository;
        private readonly ILogger<ClusterMonitoringBusiness> logger;

        public ClusterMonitoringBusiness(IClusterRepository clusterRepository, ILogger<ClusterMonitoringBusiness> logger, IClusterNodeRepository clusterNodeRepository, IMetricRepository metricRepository)
        {
            this.clusterRepository = clusterRepository;
            this.logger = logger;
            this.clusterNodeRepository = clusterNodeRepository;
            this.metricRepository = metricRepository;
        }

        public async Task MonitorClustersAsync(CancellationToken cancellationToken)
        {
            if (!Directory.Exists("./configs"))
                Directory.CreateDirectory("./configs");

            var clusters = await clusterRepository.ReadsAsync(c => !string.IsNullOrEmpty(c.KubeConfigJson) && c.DeleteAt == null);
            var clusterTask = new List<Task>();

            foreach (var cls in clusters)
            {
                await MonitorClusterAsync(cls);
            }
        }

        private async Task MonitorClusterAsync(Cluster cluster)
        {
            try
            {
                if (!Directory.Exists($"./configs/{cluster.Id}"))
                    Directory.CreateDirectory($"./configs/{cluster.Id}");

                var filePath = $"./configs/{cluster.Id}/kubeconfig";

                if (!File.Exists(filePath))
                {
                    var kubeconfigContent = cluster.KubeConfig.Replace($"server: https://{cluster.Name}.{cluster.Domain}:6443", $"server: https://{cluster.Ip}:6443");
                    await File.WriteAllTextAsync(filePath, kubeconfigContent);
                }

                var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(filePath);
                var client = new Kubernetes(config);

                var clusterNodes = await client.ListNodeAsync();

                await ProcessStateAsync(cluster, clusterNodes);
                await ProcessMetricsAsync(cluster, client);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Fail to connect client.");
            }
        }

        private async Task ProcessStateAsync(Cluster cluster, k8s.Models.V1NodeList clusterNodes)
        {
            try
            {
                foreach (var clusterNode in clusterNodes.Items)
                {
                    logger.LogInformation($"Node => {JsonSerializer.Serialize(clusterNode)}");
                    if (clusterNode.Metadata.Name.Contains("master"))
                    {
                        cluster.State = clusterNode.Status.Conditions.FirstOrDefault(c => c.Reason == "KubeletReady")?.Type;
                        var _ = await clusterRepository.UpdateAsync(cluster);
                    }
                    else
                    {
                        var node = cluster.Nodes.FirstOrDefault(n => n.Name == clusterNode.Metadata.Name);
                        if (node != null)
                        {
                            node.State = clusterNode.Status.Conditions.FirstOrDefault(c => c.Reason == "KubeletReady")?.Type;
                            var __ = await clusterNodeRepository.UpdateAsync(node);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error on processing state");
            }
        }

        private async Task ProcessMetricsAsync(Cluster cluster, Kubernetes client)
        {
            try
            {
                var metrics = await client.GetKubernetesNodesMetricsAsync();
                var metricsGathered = new List<Metric>();

                foreach (var metric in metrics.Items)
                {
                    var node = cluster.Nodes.FirstOrDefault(n => n.Name == metric.Metadata.Name);
                    var extractItemId = string.Empty;

                    if (node != null)
                        extractItemId = node.Id;
                    else if (metric.Metadata.Name.Contains("master"))
                        extractItemId = cluster.Id;

                    if (!string.IsNullOrEmpty(extractItemId))
                    {
                        metricsGathered.Add(new Metric()
                        {
                            EntityId = extractItemId,
                            CpuValue = long.Parse(metric.Usage["cpu"].CanonicalizeString().Replace("n", "")),
                            MemoryValue = long.Parse(metric.Usage["memory"].CanonicalizeString().Replace("Ki", ""))
                        });

                    }
                }

                await metricRepository.InsertMetricsWithStrategyAsync(metricsGathered.ToArray());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error on processing state");
            }
        }
    }
}
