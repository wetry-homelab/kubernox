using Docker.DotNet;
using Docker.DotNet.Models;
using Kubernox.Interfaces;
using Kubernox.Model;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Kubernox.Services
{
    public class ContainerDeploymentService : IContainerDeploymentService
    {
        private readonly Logger logger = new LoggerConfiguration()
                                        .WriteTo.File("kubernox.log")
                                        .WriteTo.Console()
                                        .CreateLogger();

        private readonly DockerClient client;

        private const string DbContainerName = "kubernox_db";
        private const string QueueContainerName = "kubernox_queue";
        private const string CacheContainerName = "kubernox_cache";
        private const string PrometheusContainerName = "kubernox_prometheus";
        private const string GrafanaContainerName = "kubernox_grafana";
        private const string ServiceContainerName = "kubernox_service";
        private const string WorkersContainerName = "kubernox_workers";
        private const string KubernoxContainerName = "kubernox";
        private const string KubernoxDeployWorkerContainerName = "kubernox_deploy_worker";
        private const string TraefikContainerName = "kubernox_traefik";
        private const string NetworkName = "kubernox_lan";

        public ContainerDeploymentService()
        {
            client = new DockerClientConfiguration(new Uri(GetSocket())).CreateClient();
        }

        private string GetSocket()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return "npipe://./pipe/docker_engine";

            return "unix:///var/run/docker.sock";
        }

        public async Task<bool> InstantiateNetworkAsync()
        {
            var networks = await client.Networks.ListNetworksAsync();

            if (networks.Any(a => a.Name == NetworkName))
                return true;

            var network = await client.Networks.CreateNetworkAsync(new NetworksCreateParameters()
            {
                Name = NetworkName
            });

            return network != null;
        }

        public async Task<bool> InstantiateDatabaseContainer(PostgreDatabaseProvider database, CancellationToken cancellationToken)
        {
            logger.Information("---- Starting Deploy Database Container ----");

            await DownloadImage("postgres", "latest", cancellationToken);

            var volumes = new Dictionary<string, EmptyStruct>();
            volumes.Add($"{database.Storage}:/var/lib/postgresql/data", new EmptyStruct());

            var ports = new Dictionary<string, EmptyStruct>();
            ports.Add("5432/tcp", new EmptyStruct());

            var createParameters = new CreateContainerParameters()
            {
                Image = "postgres:latest",
                Name = DbContainerName,
                Hostname = DbContainerName,
                Env = new List<string>() { $"POSTGRES_PASSWORD={database.Password}", $"POSTGRES_USER={database.Username}", $"POSTGRES_DB={database.DbName}" },
                Volumes = volumes,
                ExposedPorts = ports,
                HostConfig = new HostConfig()
                {
                    NetworkMode = NetworkName,
                    RestartPolicy = new RestartPolicy()
                    {
                        Name = RestartPolicyKind.Always
                    },
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            $"5432/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = $"5432"
                                }
                            }
                        }
                    }
                }
            };

            return await DeployAndStartAsync(DbContainerName, createParameters, cancellationToken);
        }

        public async Task<bool> InstantiateQueueContainer(RabbitMqProvider queue, CancellationToken cancellationToken)
        {
            logger.Information("---- Starting Deploy Queue Container ----");
            await DownloadImage("rabbitmq", "3-management", cancellationToken);
            var ports = new Dictionary<string, EmptyStruct>();
            ports.Add($"{queue.Port}/tcp", new EmptyStruct());
            ports.Add($"15672/tcp", new EmptyStruct());

            var createParameters = new CreateContainerParameters()
            {
                Image = "rabbitmq:3-management",
                ExposedPorts = ports,
                Name = QueueContainerName,
                Hostname = QueueContainerName,
                HostConfig = new HostConfig()
                {
                    NetworkMode = NetworkName,
                    RestartPolicy = new RestartPolicy()
                    {
                        Name = RestartPolicyKind.Always
                    },
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            $"{queue.Port}/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = $"{queue.Port}"
                                }
                            }
                        },
                        {
                            $"15672/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = $"15672"
                                }
                            }
                        }
                    }
                },
                Env = new List<string>() { $"RABBITMQ_DEFAULT_USER={queue.Username}", $"RABBITMQ_DEFAULT_PASS={queue.Password}" },
            };

            return await DeployAndStartAsync(QueueContainerName, createParameters, cancellationToken);
        }

        public async Task<bool> InstantiateCacheContainer(RedisProvider redis, CancellationToken cancellationToken)
        {
            logger.Information("---- Starting Deploy Cache Container ----");
            await DownloadImage("redis", cancellationToken);

            var ports = new Dictionary<string, EmptyStruct>();
            ports.Add("6379/tcp", new EmptyStruct());

            var createParameters = new CreateContainerParameters()
            {
                Image = "redis",
                Name = CacheContainerName,
                Cmd = new List<string>() { $"redis-server", "--requirepass", $"{redis.Password}" },
                Hostname = CacheContainerName,
                ExposedPorts = ports,
                HostConfig = new HostConfig()
                {
                    NetworkMode = NetworkName,
                    RestartPolicy = new RestartPolicy()
                    {
                        Name = RestartPolicyKind.Always
                    },
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            "6379/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = "6379"
                                }
                            }
                        }
                    }
                }
            };

            return await DeployAndStartAsync(CacheContainerName, createParameters, cancellationToken);
        }

        public async Task<bool> InstantiatePrometheusContainer(PrometheusProvider prometheus, CancellationToken cancellationToken)
        {
            logger.Information("---- Starting Deploy Prometheus Container ----");
            await DownloadImage("prom/prometheus", "latest", cancellationToken);


            var ports = new Dictionary<string, EmptyStruct>();
            ports.Add("9090/tcp", new EmptyStruct());

            var createParameters = new CreateContainerParameters()
            {
                Image = "prom/prometheus:latest",
                Name = PrometheusContainerName,
                Hostname = PrometheusContainerName,
                ExposedPorts = ports,
                HostConfig = new HostConfig()
                {
                    NetworkMode = NetworkName,
                    Binds = new List<string>()
                    {
                        $"{prometheus.Path}:/etc/prometheus/prometheus.yml"
                    },
                    RestartPolicy = new RestartPolicy()
                    {
                        Name = RestartPolicyKind.Always
                    },
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            "9090/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = "9090"
                                }
                            }
                        }
                    }
                }
            };

            return await DeployAndStartAsync(PrometheusContainerName, createParameters, cancellationToken);
        }

        public async Task<bool> InstantiateGrafanaContainer(PrometheusProvider prometheus, CancellationToken cancellationToken)
        {
            logger.Information("---- Starting Deploy Grafana Container ----");
            await DownloadImage("grafana/grafana", "latest", cancellationToken);


            var ports = new Dictionary<string, EmptyStruct>();
            ports.Add("3000/tcp", new EmptyStruct());

            var createParameters = new CreateContainerParameters()
            {
                Image = "grafana/grafana:latest",
                Name = GrafanaContainerName,
                Hostname = GrafanaContainerName,
                ExposedPorts = ports,
                Env = new List<string>()
                {
                    "GF_INSTALL_PLUGINS=grafana-clock-panel,grafana-simple-json-datasource,grafana-piechart-panel"
                },
                HostConfig = new HostConfig()
                {
                    NetworkMode = NetworkName,
                    Binds = new List<string>()
                    {
                        $"{prometheus.Path}:/etc/prometheus/prometheus.yml"
                    },
                    RestartPolicy = new RestartPolicy()
                    {
                        Name = RestartPolicyKind.Always
                    },
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            "3000/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = "3000"
                                }
                            }
                        }
                    }
                }
            };

            return await DeployAndStartAsync(GrafanaContainerName, createParameters, cancellationToken);
        }

        public async Task<bool> InstantiateKubernoxServiceContainer(Configuration configuration, CancellationToken cancellationToken)
        {
            logger.Information("---- Starting Deploy Kubernox Container ----");
            await DownloadImage("kubernox/kubernox-service", cancellationToken);
            var ports = new Dictionary<string, EmptyStruct>();
            ports.Add("80/tcp", new EmptyStruct());

            var createParameters = new CreateContainerParameters()
            {
                Image = "kubernox/kubernox-service",
                Name = ServiceContainerName,
                ExposedPorts = ports,
                HostConfig = StackHostConfig(),
                Hostname = ServiceContainerName,
                Env = new List<string>()
                {
                    $"Kubernox__Domain={configuration.Kubernox.Domain}",
                    $"Proxmox__Uri={configuration.Proxmox.Host}",
                    $"Proxmox__Token=PVEAPIToken={configuration.Proxmox.Username}@{configuration.Proxmox.AuthType}!{configuration.Proxmox.TokenId}={configuration.Proxmox.AccessToken}",
                    $"ConnectionStrings__Default=Host={configuration.Postgre.Host};Database={configuration.Postgre.DbName};Username={configuration.Postgre.Username};Password={configuration.Postgre.Password}",
                    $"ConnectionStrings__Redis={configuration.Redis.Host},password={configuration.Redis.Password}",
                    $"RabbitMq__User={configuration.Rabbitmq.Username}",
                    $"RabbitMq__Password={configuration.Rabbitmq.Password}",
                    $"RabbitMq__HostName={configuration.Rabbitmq.Host}",
                    $"RabbitMq__VirtualHost={configuration.Rabbitmq.Virtualhost}",
                    $"RabbitMq__Port={configuration.Rabbitmq.Port}"
                }
            };

            return await DeployAndStartAsync(ServiceContainerName, createParameters, cancellationToken);
        }

        public async Task<bool> InstantiateKubernoxWorkersContainer(Configuration configuration, CancellationToken cancellationToken)
        {
            logger.Information("---- Starting Deploy Kubernox Datacenter Worker Container ----");
            await DownloadImage("kubernox/kubernox-workers", cancellationToken);

            var createParameters = new CreateContainerParameters()
            {
                Image = "kubernox/kubernox-workers",
                Name = WorkersContainerName,
                HostConfig = StackHostConfig(),
                Env = new List<string>()
                {
                    $"Proxmox__Uri={configuration.Proxmox.Host}",
                    $"Proxmox__Token=PVEAPIToken={configuration.Proxmox.Username}@{configuration.Proxmox.AuthType}!{configuration.Proxmox.TokenId}={configuration.Proxmox.AccessToken}",
                    $"ConnectionStrings__Default=Host={configuration.Postgre.Host};Database={configuration.Postgre.DbName};Username={configuration.Postgre.Username};Password={configuration.Postgre.Password}",
                }
            };

            return await DeployAndStartAsync(WorkersContainerName, createParameters, cancellationToken);
        }

        public async Task<bool> InstantiateKubernoxUiContainer(Configuration configuration, CancellationToken cancellationToken)
        {
            logger.Information("---- Starting Deploy Kubernox Container ----");
            await DownloadImage("kubernox/kubernox", cancellationToken);

            var ports = new Dictionary<string, EmptyStruct>();
            ports.Add("80/tcp", new EmptyStruct());

            var createParameters = new CreateContainerParameters()
            {
                Image = "kubernox/kubernox",
                Name = KubernoxContainerName,
                Hostname = KubernoxContainerName,
                ExposedPorts = ports,
                HostConfig = new HostConfig()
                {
                    RestartPolicy = new RestartPolicy()
                    {
                        Name = RestartPolicyKind.Always
                    },
                    NetworkMode = NetworkName,
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            "80/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = $"{configuration.Kubernox.UiPortExpose}"
                                }
                            }
                        }
                    }
                }
            };

            return await DeployAndStartAsync(KubernoxContainerName, createParameters, cancellationToken);
        }

        public async Task<bool> InstantiateDeployWorkerContainer(Configuration configuration, CancellationToken cancellationToken)
        {
            logger.Information("---- Starting Deploy Deploy Worker Container ----");
            await DownloadImage("kubernox/kubernox-deploy-worker", "latest", cancellationToken);

            var createParameters = new CreateContainerParameters()
            {
                Image = "kubernox/kubernox-deploy-worker:latest",
                Name = KubernoxDeployWorkerContainerName,
                Hostname = KubernoxDeployWorkerContainerName,
                Env = new List<string>()
                {
                    $"Proxmox__APIUser={configuration.Proxmox.Username}@{configuration.Proxmox.AuthType}",
                    $"Proxmox__APIPassword={configuration.Proxmox.Password}",
                    $"Proxmox__APIUrl={configuration.Proxmox.Host}/api2/json",
                    $"RabbitMq__Url=amqp://{configuration.Rabbitmq.Username}:{configuration.Rabbitmq.Password}@127.0.0.1:{configuration.Rabbitmq.Port}"
                },
                HostConfig = new HostConfig()
                {
                    NetworkMode = "host",
                    RestartPolicy = new RestartPolicy()
                    {
                        Name = RestartPolicyKind.Always
                    }
                }
            };


            return await DeployAndStartAsync(KubernoxDeployWorkerContainerName, createParameters, cancellationToken);
        }

        public async Task<bool> InstantiateTraefikProxyContainer(Configuration configuration, CancellationToken cancellationToken)
        {
            logger.Information("---- Starting Deploy Traefik Container ----");
            await DownloadImage("traefik:latest", cancellationToken);

            var ports = new Dictionary<string, EmptyStruct>();
            ports.Add("8080/tcp", new EmptyStruct());
            ports.Add("443/tcp", new EmptyStruct());
            ports.Add("80/tcp", new EmptyStruct());

            var createParameters = new CreateContainerParameters()
            {
                Image = "traefik:latest",
                Name = TraefikContainerName,
                Hostname = TraefikContainerName,
                ExposedPorts = ports,
                Labels = new Dictionary<string, string>()
                {
                    { "traefik.http.routers.traefik.rule", $"Host(`{configuration.Kubernox.Domain}`) && Path(`/traefik`)" },
                    { "traefik.http.routers.traefik.tls", "true" },
                    { "traefik.http.routers.traefik.tls.certresolver", "gandiResolver" },
                    { "traefik.http.routers.traefik.tls.domains[0].main", $"{configuration.Kubernox.Domain}" },
                    { "traefik.http.routers.traefik.tls.domains[0].sans", $"*.{configuration.Kubernox.Domain}" }
                },
                Env = new List<string>()
                {
                    $"GANDIV5_API_KEY={configuration.Traefik.ApiKey}"
                },
                HostConfig = new HostConfig()
                {
                    NetworkMode = "host",
                    Binds = new List<string>()
                    {
                        $"{configuration.Traefik.Path}:/etc/traefik/traefik.yml",
                        "/var/run/docker.sock:/var/run/docker.sock",
                        "/home/tech/traefik-conf:/conf"
                    },
                    RestartPolicy = new RestartPolicy()
                    {
                        Name = RestartPolicyKind.Always
                    },
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            "8080/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = $"8080"
                                }
                            }
                        },
                        {
                            "443/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = $"443"
                                }
                            }
                        },
                        {
                            "80/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = $"80"
                                }
                            }
                        }
                    }
                }
            };

            return await DeployAndStartAsync(TraefikContainerName, createParameters, cancellationToken);
        }

        private async Task DownloadImage(string image, CancellationToken cancellationToken)
        {
            var progress = new Progress<JSONMessage>();

            await client.Images.CreateImageAsync(new ImagesCreateParameters()
            {
                FromImage = image,
                Tag = "latest"
            }, null, progress, cancellationToken);

            progress.ProgressChanged += Progress_ProgressChanged;
        }

        private async Task DownloadImage(string image, string tag, CancellationToken cancellationToken)
        {
            var progress = new Progress<JSONMessage>();

            await client.Images.CreateImageAsync(new ImagesCreateParameters()
            {
                FromImage = image,
                Tag = tag
            }, null, progress, cancellationToken);

            progress.ProgressChanged += Progress_ProgressChanged;
        }

        private void Progress_ProgressChanged(object sender, JSONMessage e)
        {
            logger.Information($"{e.ProgressMessage}");
        }

        private HostConfig StackHostConfig()
        {
            return new HostConfig()
            {
                NetworkMode = NetworkName,
                RestartPolicy = new RestartPolicy()
                {
                    Name = RestartPolicyKind.Always
                }
            };
        }

        private async Task<bool> DeployAndStartAsync(string name, CreateContainerParameters parameters, CancellationToken cancellationToken)
        {
            Exception error = null;

            do
            {
                try
                {
                    IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
                                                                                        new ContainersListParameters()
                                                                                        {
                                                                                            Limit = 1000
                                                                                        });

                    var container = containers.FirstOrDefault(c => c.Names.Contains($"/{name}"));

                    if (container == null)
                    {
                        var containerCreateResult = await client.Containers.CreateContainerAsync(parameters, cancellationToken);
                        logger.Information($"------ {name} creation done");
                    }
                    else
                    {
                        logger.Information($"------ {name} already exist.");
                    }

                    if (container != null && container.State == "running")
                    {
                        logger.Information($"------ {name} running");
                        return true;
                    }

                    var started = await client.Containers.StartContainerAsync(name, null, cancellationToken);

                    if (started)
                    {
                        logger.Information($"------ {name} running");
                        return true;
                    }
                }
                catch (Exception e)
                {
                    error = e;
                    logger.Error(e, e.Message);
                }
            } while (error != null && !cancellationToken.IsCancellationRequested);

            return false;
        }
    }
}
