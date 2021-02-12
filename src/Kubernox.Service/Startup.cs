using Application.Interfaces;
using Application.Mappers;
using AutoMapper;
using Kubernox.Service.Business;
using Kubernox.Service.Hubs;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Infrastructure.Persistence.Contexts;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

            services.AddCors(cors =>
            {
                cors.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddSignalR();

            AddBusinessLayer(services);
        }

        private void AddBusinessLayer(IServiceCollection services)
        {
            services.AddScoped<IClusterBusiness, ClusterBusiness>();
            services.AddScoped<IDatacenterBusiness, DatacenterBusiness>();
            services.AddScoped<ISshKeyBusiness, SshKeyBusiness>();
            services.AddScoped<ITemplateBusiness, TemplateBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ServiceDbContext serviceDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kubernox.Service v1"));
            }

            MigrateDatabase(serviceDbContext);

            app.UseHttpsRedirection();

            app.UseCors();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AppHub>("/app");
            });
        }

        private void MigrateDatabase(ServiceDbContext serviceDbContext)
        {
            try
            {
                serviceDbContext.Database.Migrate();

                if (!serviceDbContext.Template.Any())
                {
                    serviceDbContext.Template.Add(new Domain.Entities.Template()
                    {
                        BaseTemplate = "k3s-template",
                        CpuCount = 1,
                        DiskSpace = 20,
                        MemoryCount = 1024,
                        Name = "Small",
                        Type = "k3s"
                    });

                    serviceDbContext.Template.Add(new Domain.Entities.Template()
                    {
                        BaseTemplate = "k3s-template",
                        CpuCount = 1,
                        DiskSpace = 30,
                        MemoryCount = 2048,
                        Name = "Medium",
                        Type = "k3s"
                    });

                    serviceDbContext.Template.Add(new Domain.Entities.Template()
                    {
                        BaseTemplate = "k3s-template",
                        CpuCount = 2,
                        DiskSpace = 40,
                        MemoryCount = 4096,
                        Name = "Large",
                        Type = "k3s"
                    });

                    serviceDbContext.Template.Add(new Domain.Entities.Template()
                    {
                        BaseTemplate = "k3s-template",
                        CpuCount = 4,
                        DiskSpace = 50,
                        MemoryCount = 4096,
                        Name = "XLarge",
                        Type = "k3s"
                    });

                    serviceDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
