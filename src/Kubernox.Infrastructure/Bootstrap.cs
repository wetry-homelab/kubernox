using Kubernox.Infrastructure.Database;
using Kubernox.Infrastructure.Interfaces;
using Kubernox.Infrastructure.Repositories;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kubernox.Infrastructure
{
    public static class Bootstrap
    {
        public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KubernoxDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Default"), pgsql =>
                {
                    pgsql.MigrationsAssembly(typeof(Bootstrap).Assembly.GetName().Name);
                });
            }, ServiceLifetime.Singleton);

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IHostConfigurationRepository, HostConfigurationRepository>();
            services.AddScoped<INodeRepository, NodeRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();

            return services;
        }

        public static IApplicationBuilder MigrateData(this IApplicationBuilder builder)
        {
            var dbContext = builder.ApplicationServices.GetRequiredService<KubernoxDbContext>();
            dbContext.Database.Migrate();

            if (!dbContext.HostConfigurations.Any())
            {
                dbContext.HostConfigurations.Add(new Domain.Entities.HostConfiguration()
                {
                    ApiToken = "root@pam!dev=2c29ffce-b72a-4acd-8fdc-c4a1ab5b4cdb",
                    Ip = "192.168.50.200",
                    CreateAt = DateTime.UtcNow,
                    CreateBy = "System",
                    IsActive = true,
                    Name = "Home",
                    Id = Guid.NewGuid()
                });

                dbContext.SaveChanges();
            }

            return builder;
        }
    }
}
