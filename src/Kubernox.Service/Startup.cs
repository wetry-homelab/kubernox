using Application.Interfaces;
using Application.Mappers;
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
using Serilog.Ui.PostgreSqlProvider.Extensions;
using Serilog.Ui.Web;
using System;
using System.Configuration;

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
            services.AddSerilogUi(options =>
            {
                options.UseNpgSql(Configuration.GetConnectionString("Default"), "logs");
            });

            ConfigureCors(services);
            AddBusinessLayer(services);

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
                endpoints.MapHub<AppHub>("/ws/notifications");
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