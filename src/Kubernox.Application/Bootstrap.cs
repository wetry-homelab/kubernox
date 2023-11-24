using Hangfire;

using Kubernox.Application.Clients;
using Kubernox.Application.Interfaces;
using Kubernox.Application.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kubernox.Application
{
    public static class Bootstrap
    {
        public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IProxmoxClient, ProxmoxClient>();
            services.AddSingleton<IProjectNameService, ProjectNameService>();
            services.AddScoped<ISyncHostWorker, SyncHostWorker>();
            services.AddScoped<ISyncTemplateWorker, SyncTemplateWorker>();

            return services;
        }

        public static IApplicationBuilder RegisterCoreJobs(this IApplicationBuilder builder)
        {
            var recurringJobManager = builder.ApplicationServices.GetRequiredService<IRecurringJobManager>();
            recurringJobManager.AddOrUpdate<ISyncHostWorker>(typeof(ISyncHostWorker).Name, job => job.ProcessHostAsync(), Cron.Minutely);
            recurringJobManager.AddOrUpdate<ISyncTemplateWorker>(typeof(ISyncTemplateWorker).Name, job => job.ProcessTemplateAsync(), Cron.Minutely);
            return builder;
        }
    }
}
