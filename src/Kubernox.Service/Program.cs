using Application.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Shared.Services;
using Kubernox.Service.Business;
using Kubernox.Service.Services;
using Kubernox.Service.Workers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Kubernox.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     config.AddEnvironmentVariables();
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices((services) =>
                {
                    services.AddSignalR();
                    services.AddScoped<IDatacenterRepository, DatacenterRepository>();
                    services.AddScoped<ITraefikRouteValueRepository, TraefikRouteValueRepository>();
                    services.AddScoped<IClusterRepository, ClusterRepository>();
                    services.AddScoped<IQueueService, QueueService>();
                    services.AddScoped<ITraefikRouterService, TraefikRouterService>();
                    services.AddScoped<IQueueBusiness, QueueBusiness>();
                    services.AddHostedService<ClusterQueueWorker>();
                    services.AddHostedService<TraefikRoutingWorker>();
                })
                .UseSerilog((hostingContext, loggerConfiguration) =>
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
    }
}
