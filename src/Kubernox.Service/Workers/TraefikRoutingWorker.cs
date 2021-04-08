using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kubernox.Service.Workers
{
    public class TraefikRoutingWorker : BackgroundService
    {
        private readonly ILogger<TraefikRoutingWorker> logger;
        public readonly IServiceProvider services;

        public TraefikRoutingWorker(IServiceProvider services,
            ILogger<TraefikRoutingWorker> logger)
        {
            this.services = services;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Starting sync traefik routing service.");
            await ExecuteSync(stoppingToken);
        }

        private async Task ExecuteSync(CancellationToken stoppingToken)
        {
            do
            {
                try
                {
                    using (var scope = services.CreateScope())
                    {
                        var scopedProcessingService =
                            scope.ServiceProvider
                                .GetRequiredService<ITraefikRouterService>();
                        await scopedProcessingService.LoadAndRefreshRouting(stoppingToken);
                    }
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Unable to sync traefik routing service.");
                }

                await Task.Delay(TimeSpan.FromMinutes(10));

            } while (!stoppingToken.IsCancellationRequested);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Stopping sync traefik routing service.");
            await base.StopAsync(stoppingToken);
        }
    }
}
