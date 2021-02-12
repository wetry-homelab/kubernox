using Application.Interfaces;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Kubernox.Workers.Business;
using Kubernox.Workers.Services;
using Microsoft.Extensions.Configuration;

namespace Kubernox.Workers
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
                }).ConfigureServices(services =>
                {
                    services.AddScoped<IClusterRepository, ClusterRepository>();
                    services.AddScoped<IClusterMonitoringBusiness, ClusterMonitoringBusiness>();
                    services.AddScoped<IDatacenterRepository, DatacenterRepository>();
                    services.AddScoped<IDatacenterSyncBusiness, DatacenterSyncBusiness>();

                    services.AddHostedService<DatacenterSyncWorker>();
                    services.AddHostedService<ClusterMonitoringWorker>();
                });
    }
}
