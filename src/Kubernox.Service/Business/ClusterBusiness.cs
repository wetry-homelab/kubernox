using Application.Exceptions;
using Application.Interfaces;
using Application.Messages;
using AutoMapper;
using Application.Entities;
using Infrastructure.Contracts.Request;
using Infrastructure.Contracts.Response;
using Microsoft.Extensions.Configuration;
using ProxmoxVEAPI.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.Service.Business
{
    public class ClusterBusiness : IClusterBusiness
    {
        private readonly IClusterRepository clusterRepository;
        private readonly IClusterNodeRepository clusterNodeRepository;
        private readonly IDatacenterRepository datacenterRepository;
        private readonly ISshKeyRepository sshKeyRepository;
        private readonly IMetricRepository metricRepository;
        private readonly IQueueService queueService;
        private readonly ITemplateRepository templateRepository;
        private readonly ITraefikRouterService traefikRouterService;
        private readonly IDomainRepository domainNameRepository;
        private readonly IMapper mapper;
        private readonly string domain;

        public ClusterBusiness(IClusterRepository clusterRepository, IDatacenterRepository datacenterRepository,
            ISshKeyRepository sshKeyRepository, IClusterNodeRepository clusterNodeRepository,
            IQueueService queueService, ITemplateRepository templateRepository,
            IConfiguration configuration, IMetricRepository metricRepository, IMapper mapper, ITraefikRouterService traefikRouterService, IDomainRepository domainNameRepository)
        {

            if (clusterRepository == null)
                throw new ArgumentNullException(nameof(clusterRepository));
            if (datacenterRepository == null)
                throw new ArgumentNullException(nameof(datacenterRepository));
            if (sshKeyRepository == null)
                throw new ArgumentNullException(nameof(sshKeyRepository));
            if (clusterNodeRepository == null)
                throw new ArgumentNullException(nameof(clusterNodeRepository));
            if (queueService == null)
                throw new ArgumentNullException(nameof(queueService));
            if (templateRepository == null)
                throw new ArgumentNullException(nameof(templateRepository));
            if (traefikRouterService == null)
                throw new ArgumentNullException(nameof(traefikRouterService));
            if (metricRepository == null)
                throw new ArgumentNullException(nameof(metricRepository));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            this.clusterRepository = clusterRepository;
            this.datacenterRepository = datacenterRepository;
            this.sshKeyRepository = sshKeyRepository;
            this.clusterNodeRepository = clusterNodeRepository;
            this.queueService = queueService;
            this.templateRepository = templateRepository;
            this.domain = configuration["Kubernox:Domain"];
            this.metricRepository = metricRepository;
            this.mapper = mapper;
            this.traefikRouterService = traefikRouterService;
            this.domainNameRepository = domainNameRepository;
        }

        public async Task<bool> CreateClusterAsync(ClusterCreateRequest request)
        {
            if ((await clusterRepository.ReadAsync(c => c.Name == request.Name)) != null)
                throw new DuplicateException();

            var selectedSshKey = await sshKeyRepository.ReadAsync(request.SshKeyId);
            var selectedNode = await datacenterRepository.ReadAsync(f => f.Id == request.DeployNodeId);
            var template = await templateRepository.ReadAsync(t => t.Id == request.SelectedTemplate);
            var existingCluster = await clusterRepository.ReadsAsync(c => c.ProxmoxNodeId == request.DeployNodeId);
            var domainDb = await domainNameRepository.ReadAsync(d => d.Id == request.LinkDomainId);

            var baseIp = existingCluster.Any() ? GetBasedRangeIp(existingCluster) : 10;
            var baseId = existingCluster.Any() ? ExtractBaseId(await clusterRepository.GetMaxOrder()) : 3000;

            if (baseIp > 0 && baseId > 0)
            {
                var newCluster = new Cluster()
                {
                    Cpu = request.Cpu,
                    Name = $"k3s-master-{request.Name}",
                    Node = request.Node,
                    Storage = request.Storage,
                    Domain = domainDb != null ? domainDb.Value : domain,
                    User = "root",
                    ProxmoxNodeId = selectedNode.Id,
                    OrderId = baseId,
                    Memory = request.Memory,
                    Ip = $"10.0.{selectedNode.Id}.{baseIp}",
                    SshKeyId = selectedSshKey.Id,
                    BaseTemplate = template.BaseTemplate
                };

                if ((await clusterRepository.InsertAsync(newCluster)) > 0)
                {
                    var nodeList = new List<ClusterNode>();

                    for (int i = 0; i < request.Node; i++)
                    {
                        nodeList.Add(new ClusterNode()
                        {
                            ClusterId = newCluster.Id,
                            OrderId = baseId + i + 1,
                            Name = $"k3s-node{i + 1}-{request.Name}",
                            Ip = $"10.0.{selectedNode.Id}.{baseIp + i + 1}",
                        });
                    }

                    if ((await clusterNodeRepository.InsertsAsync(nodeList.ToArray()) == request.Node))
                    {
                        ClusterMessage message = GenerateCreateQueueMessage(request, newCluster, selectedSshKey, selectedNode, template, baseIp, baseId);
                        queueService.QueueClusterCreation(message);

                        await traefikRouterService.GenerateClusterBasicRules(newCluster);
                    }

                    return true;
                }
            }

            return false;
        }

        public async Task<ClusterDetailsResponse> GetClusterAsync(string id)
        {
            var cluster = await clusterRepository.ReadAsync(c => c.Id == id && c.DeleteAt == null);

            if (cluster != null)
            {
                var clusterMetrics = await metricRepository.ReadsAsync(m => m.EntityId == id);
                var response = mapper.Map<ClusterDetailsResponse>(cluster);

                response.MasterMetrics = clusterMetrics.Select(cm => mapper.Map<SimpleMetricItemResponse>(cm))
                                            .OrderByDescending(o => o.DateValue)
                                            .Take(10).ToList();
                response.Nodes = new List<ClusterNodeDetailsResponse>();

                foreach (var node in cluster.Nodes)
                {
                    var nodeMetrics = await metricRepository.ReadsAsync(m => m.EntityId == node.Id);
                    var nodeMapped = mapper.Map<ClusterNodeDetailsResponse>(node);
                    nodeMapped.NodeMetrics = nodeMetrics.Select(cm => mapper.Map<SimpleMetricItemResponse>(cm))
                                                .OrderByDescending(o => o.DateValue)
                                                .Take(10).ToList();
                    response.Nodes.Add(nodeMapped);
                }

                return response;
            }

            return null;
        }

        public async Task<bool> RefreshClusterRulesAsync(string id)
        {
            var cluster = await clusterRepository.ReadAsync(c => c.Id == id && c.DeleteAt == null);

            if (cluster != null)
            {
                await traefikRouterService.RefreshClusterRule(id);
                return true;
            }

            return false;
        }

        private int ExtractBaseId(int valueMaxOrder)
        {
            return valueMaxOrder + 10;
        }

        private int GetBasedRangeIp(Cluster[] existingCluster)
        {
            for (int i = 10; i < 240; i += 10)
            {
                if (!existingCluster.Any(c => c.Ip == $"10.0.{c.ProxmoxNodeId}.{i}"))
                {
                    return i;
                }
            }

            return -1;
        }

        private ClusterMessage GenerateCreateQueueMessage(ClusterCreateRequest request, Cluster newCluster, SshKey selectedSshKey, DatacenterNode selectedNode, Template template, int baseIp, int baseId)
        {

            var message = new ClusterMessage()
            {
                Pattern = "create",
                Data = new Data()
                {
                    Id = baseId,
                    Name = request.Name,
                    Features = new Feature()
                    {
                        Traefik = request.InstallTraefik,
                    },
                    Config = new Config()
                    {
                        User = "root",
                        Password = "homelab0123",
                        Ssh = new Ssh()
                        {
                            PrivateKey = selectedSshKey.Pem,
                            PublicKey = selectedSshKey.Public
                        }
                    },
                    Nodes = new List<Node>()
                }
            };

            var master = new Node()
            {
                Id = baseId,
                CpuCores = 2,
                Master = true,
                Disk = 30,
                Memory = 2048,
                Ip = $"10.0.{selectedNode.Id}.{baseIp}",
                ProxmoxNode = selectedNode.Name,
                Template = template.BaseTemplate
            };

            message.Data.Nodes.Add(master);

            for (var i = 0; i < newCluster.Node; i++)
            {
                var node = new Node()
                {
                    Id = baseId + i + 1,
                    CpuCores = newCluster.Cpu,
                    Master = false,
                    Disk = newCluster.Storage,
                    Memory = newCluster.Memory,
                    Ip = $"10.0.{selectedNode.Id}.{baseIp + i + 1}",
                    ProxmoxNode = selectedNode.Name,
                    Template = template.BaseTemplate
                };

                message.Data.Nodes.Add(node);
            }

            return message;
        }

        private ClusterMessage GenerateDeleteQueueMessage(Cluster cluster, SshKey selectedSshKey, DatacenterNode selectedNode, int baseIp, int baseId)
        {
            var message = new ClusterMessage()
            {
                Pattern = "delete",
                Data = new Data()
                {
                    Id = baseId,
                    Name = cluster.Name,
                    Config = new Config()
                    {
                        User = "root",
                        Password = "homelab0123",
                        Ssh = new Ssh()
                        {
                            PrivateKey = selectedSshKey.Pem,
                            PublicKey = selectedSshKey.Public
                        }
                    },
                    Nodes = new List<Node>()
                }
            };

            var master = new Node()
            {
                Id = baseId,
                CpuCores = 2,
                Master = true,
                Disk = 30,
                Memory = 2048,
                Ip = $"10.0.{selectedNode.Id}.{baseIp}",
                ProxmoxNode = selectedNode.Name,
                Template = cluster.BaseTemplate
            };

            message.Data.Nodes.Add(master);

            for (var i = 0; i < cluster.Node; i++)
            {
                var node = new Node()
                {
                    Id = baseId + i + 1,
                    CpuCores = cluster.Cpu,
                    Master = false,
                    Disk = cluster.Storage,
                    Memory = cluster.Memory,
                    Ip = $"10.0.{selectedNode.Id}.{baseIp + i + 1}",
                    ProxmoxNode = selectedNode.Name,
                    Template = cluster.BaseTemplate
                };

                message.Data.Nodes.Add(node);
            }

            return message;
        }

        public async Task<ClusterItemResponse[]> ListClusterAsync()
        {
            var dbClusters = await clusterRepository.ReadsAsync(c => !c.DeleteAt.HasValue);

            return dbClusters.Select(
                s => new ClusterItemResponse()
                {
                    Id = s.Id,
                    Disk = s.Storage,
                    State = s.State,
                    Cpu = s.Cpu,
                    Memory = s.Memory,
                    Ip = s.Ip,
                    Name = s.Name,
                    Nodes = s.Nodes.Select(n => new ClusterNodeItemResponse()
                    {
                        Id = n.Id,
                        Name = n.Name,
                        Ip = n.Ip,
                        State = n.State
                    }).ToList()
                }).ToArray();
        }

        public Task<(bool found, bool update)> UpdateClusterAsync(string id, ClusterUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool found, bool update)> DeleteClusterAsync(string id)
        {
            var cluster = await clusterRepository.ReadAsync(c => c.Id == id);

            if (cluster == null)
                return (false, false);

            cluster.DeleteAt = DateTime.UtcNow;

            if ((await clusterRepository.UpdateAsync(cluster)) > 0)
            {
                await traefikRouterService.DeleteClusterRules(cluster);

                var selectedSshKey = await sshKeyRepository.ReadAsync(cluster.SshKeyId);
                var selectedNode = await datacenterRepository.ReadAsync(f => f.Id == cluster.ProxmoxNodeId);

                var nodes = await clusterNodeRepository.ReadsAsync(c => c.ClusterId == cluster.Id && c.DeleteAt == null);
                var qemuClient = new QemuClient();

                foreach (var node in nodes)
                {
                    node.DeleteAt = DateTime.UtcNow;
                    await clusterNodeRepository.UpdateAsync(node);
                }

                var message = GenerateDeleteQueueMessage(cluster, selectedSshKey, selectedNode, int.Parse(cluster.Ip.Split(".").Last()), cluster.ProxmoxNodeId);

                queueService.QueueClusterDelete(message);

                return (true, true);
            }

            return (true, false);
        }

        public async Task<(bool found, bool restart)> RestartClusterMasterAsync(string id)
        {
            var cluster = await clusterRepository.ReadAsync(c => c.Id == id && c.DeleteAt == null);
            var selectedNode = await datacenterRepository.ReadAsync(f => f.Id == cluster.ProxmoxNodeId);
            if (cluster == null)
                return (false, false);

            var qemuClient = new QemuClient();

            return (true, await qemuClient.RestartQemu(selectedNode.Name, cluster.OrderId));
        }

        public async Task<(bool found, bool ready, KubeconfigDownloadResponse file)> DownloadKubeconfigAsync(string id)
        {
            var cluster = await clusterRepository.ReadAsync(c => c.Id == id);
            if (cluster == null)
                return (false, false, null);

            if (string.IsNullOrEmpty(cluster.KubeConfig))
                return (true, false, null);

            return (true, true, new KubeconfigDownloadResponse()
            {
                Name = $"{cluster.Name}-kubeconfig",
                Content = cluster.KubeConfig
            });
        }
    }
}
