using Application.Interfaces;
using Application.Mappers;
using DnsClient;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Seeds;
using Infrastructure.Shared;
using Kubernox.Service.Business;
using Kubernox.Service.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using ProxmoxVEAPI.Client;
using Serilog.Ui.ElasticSearchProvider;
using Serilog.Ui.PostgreSqlProvider.Extensions;
using Serilog.Ui.Web;
using System;
using System.Configuration;
using System.Net;

namespace Kubernox.Service
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                               .AddFluentValidation();

            services.AddAutoMapper(typeof(DatacenterMapperProfile).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kubernox.Service", Version = "v1" });
            });

            services.AddSharedInfrastructure();
            services.AddPersistenceInfrastructure(Configuration);
            services.AddSignalR();
            services.AddHealthChecks();

            services.AddSerilogUi(options =>
            {
                options.UseNpgSql(Configuration.GetConnectionString("Default"), "logs");
            });

            ConfigureCors(services);
            AddBusinessLayer(services);

            services.AddSingleton<ILookupClient>(c =>
            {
                var endpoint = new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53);
                return new LookupClient(endpoint);
            });

            ConfigureClient.Initialise(Configuration["Proxmox:Uri"], Configuration["Proxmox:Token"]);
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(cors =>
            {
                cors.AddDefaultPolicy(policy =>
                {
                    policy.AllowCredentials()
                            .SetIsOriginAllowed(o => true)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });
        }

        private void AddBusinessLayer(IServiceCollection services)
        {
            services.AddScoped<IClusterBusiness, ClusterBusiness>();
            services.AddScoped<IIdentityBusiness, IdentityBusiness>();
            services.AddScoped<IDatacenterBusiness, DatacenterBusiness>();
            services.AddScoped<ISshKeyBusiness, SshKeyBusiness>();
            services.AddScoped<ITemplateBusiness, TemplateBusiness>();
            services.AddScoped<IDomaineNameBusiness, DomaineNameBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ServiceDbContext serviceDbContext)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kubernox.Service v1"));
            }

            MigrateDatabase(serviceDbContext);

            app.UseHttpsRedirection();

            app.UseCors();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSerilogUi();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AppHub>("/notifications");
                endpoints.MapHealthChecks("/health");
            });
        }

        private void MigrateDatabase(ServiceDbContext serviceDbContext)
        {
            try
            {
                serviceDbContext.Database.Migrate();
                TemplateSeed.GenerateBaseTemplateSeeds(serviceDbContext);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
