using Kubernox.Interfaces;
using Kubernox.Model;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Kubernox.Services
{
    public class OrchestratorService : IOrchestratorService
    {
        private readonly Logger logger = new LoggerConfiguration()
                                                .WriteTo.File("kubernox.log")
                                                .WriteTo.Console()
                                                .CreateLogger();

        private readonly IContainerDeploymentService containerDeploymentService = new ContainerDeploymentService();
        private Configuration configuration;

        public OrchestratorService()
        {

        }

        public async Task StartDeploymentAsync(CancellationToken cancellationToken, bool upgrade = false)
        {
            PrintHeader();
            configuration = await ExtractConfigurationAsync();

            if (upgrade)
            {
                if (!await containerDeploymentService.UpgradeProcessAsync(cancellationToken))
                {
                    logger.Error($"Upgrade failed.");
                    return;
                }
            }

            if (!await InitialiseNetworkAsync(cancellationToken))
            {
                logger.Error($"Network initialise failed.");
                return;
            }

            if (!await StartingInfrastructureStack(cancellationToken))
            {
                logger.Error($"Infrastructure stack initialise failed.");
                return;
            }

            if (!await StartingKubernoxStack(cancellationToken))
            {
                logger.Error($"Kubernox stack initialise failed.");
                return;
            }

            if (!await containerDeploymentService.InstantiateTraefikProxyContainer(configuration, cancellationToken))
            {
                logger.Error($"Traefik stack initialise failed.");
                return;
            }

            logger.Information("Deployment success.");
        }

        private Task<bool> InitialiseNetworkAsync(CancellationToken cancellationToken)
        {
            return containerDeploymentService.InstantiateNetworkAsync();
        }

        private async Task<bool> StartingInfrastructureStack(CancellationToken cancellationToken)
        {
            var deploymentStack = new List<Task<bool>>();

            deploymentStack.Add(containerDeploymentService.InstantiateDatabaseContainer(configuration.Postgre, cancellationToken));
            deploymentStack.Add(containerDeploymentService.InstantiateQueueContainer(configuration.Rabbitmq, cancellationToken));
            deploymentStack.Add(containerDeploymentService.InstantiateCacheContainer(configuration.Redis, cancellationToken));
            deploymentStack.Add(containerDeploymentService.InstantiatePrometheusContainer(configuration.Prometheus, cancellationToken));
            deploymentStack.Add(containerDeploymentService.InstantiateGrafanaContainer(configuration.Prometheus, cancellationToken));

            return (await Task.WhenAll(deploymentStack)).All(a => a);
        }

        private async Task<bool> StartingKubernoxStack(CancellationToken cancellationToken)
        {
            try
            {
                await containerDeploymentService.InstantiateKubernoxServiceContainer(configuration, cancellationToken);
                await containerDeploymentService.InstantiateKubernoxWorkersContainer(configuration, cancellationToken);
                await containerDeploymentService.InstantiateDeployWorkerContainer(configuration, cancellationToken);
                await containerDeploymentService.InstantiateKubernoxUiContainer(configuration, cancellationToken);

                return true;
            }
            catch (Exception e)
            {
                logger.Error(e, "Error when deploy kubernox stack.");
            }

            return false;
        }


        private async Task<Configuration> ExtractConfigurationAsync()
        {
            var yaml = await File.ReadAllTextAsync("kubernox.yaml");
            var deserializer = new DeserializerBuilder()
                                 .WithNamingConvention(UnderscoredNamingConvention.Instance)
                                 .Build();

            var configuration = deserializer.Deserialize<Configuration>(yaml);
            return configuration;
        }

        private void PrintHeader()
        {
            logger.Information(@"#################################################################");
            logger.Information(@"#                                                               #");
            logger.Information(@"#     ____  __.    ___.                                         #");
            logger.Information(@"#    |    |/ _|__ _\_ |__   ___________  ____   _______  ___    #");
            logger.Information(@"#    |      < |  |  \ __ \_/ __ \_  __ \/    \ /  _ \  \/  /    #");
            logger.Information(@"#    |    |  \|  |  / \_\ \  ___/|  | \/   |  (  <_> >    <     #");
            logger.Information(@"#    |____|__ \____/|___  /\___  >__|  |___|  /\____/__/\_ \    #");
            logger.Information(@"#            \/         \/     \/           \/            \/    #");
            logger.Information(@"#                                                               #");
            logger.Information(@"#          Created by David Gilson & Patrick Grasseels          #");
            logger.Information(@"#                                                               #");
            logger.Information(@"#################################################################");
        }
    }
}
