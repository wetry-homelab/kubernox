using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Polly;
using Polly.Extensions.Http;

namespace Kubernox.Shared
{
    public static class Bootstrap
    {
        public static IServiceCollection RegisterClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IKubernoxClient, KubernoxClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["Service:BaseUri"]);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(15))
            .AddPolicyHandler(GetRetryPolicy());

            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
