using Kubernox.Shared.Clients;
using Kubernox.Shared.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Kubernox.Shared
{
    public static class Bootstrap
    {
        public static IServiceCollection RegisterClientServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityClient, IdentityClient>();

            return services;
        }
    }
}
