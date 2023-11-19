using Kubernox.Application.Clients;
using Kubernox.Application.Interfaces;
using Kubernox.Application.Services;

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

            return services;
        }
    }
}
