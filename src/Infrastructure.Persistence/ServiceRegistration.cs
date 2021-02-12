using Application.Interfaces;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SQL_CONNECTION")))
            {
                connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION");
            }

            services.AddDbContext<ServiceDbContext>(options =>
                options.UseNpgsql(connectionString, postgreOpts =>
                {
                    postgreOpts.EnableRetryOnFailure(10);
                }));
     
            services.AddScoped<IClusterRepository, ClusterRepository>();
            services.AddScoped<ISshKeyRepository, SshKeyRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IDatacenterRepository, DatacenterRepository>();
            services.AddScoped<IClusterNodeRepository, ClusterNodeRepository>();
            services.AddScoped<IMetricRepository, MetricRepository>();
        }
    }
}
