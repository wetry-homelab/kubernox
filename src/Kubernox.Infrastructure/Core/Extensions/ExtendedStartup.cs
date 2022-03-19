using Kubernox.Infrastructure.Business;
using Kubernox.Infrastructure.Core.Persistence;
using Kubernox.Infrastructure.Interfaces;
using Kubernox.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;

namespace Kubernox.Infrastructure.Core.Extensions
{
    public static class ExtendedStartup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseConnectionFactory, SqlDbConnectionFactory>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHostRepository, HostRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            return services;
        }

        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IIdentityBusiness, IdentityBusiness>();
            services.AddScoped<IHostBusiness, HostBusiness>();
            services.AddScoped<ILogBusiness, LogBusiness>();
            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var secret = "asdv234234^&%&^%&^hjsdfb2%%%";
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://kubernox",
                    ValidAudience = "https://kubernox.api",
                    IssuerSigningKey = securityKey
                };
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Kubernox API",
                    Contact = new OpenApiContact
                    {
                        Name = "Kubernox",
                        Email = string.Empty,
                    }
                });

                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IServiceCollection AddAppCors(this IServiceCollection services)
        {

            services.AddCors(cors =>
            {
                cors.AddDefaultPolicy(policy =>
                {
                    policy.SetIsOriginAllowedToAllowWildcardSubdomains()
                            .SetIsOriginAllowed(d => true)
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn("UserName", SqlDbType.VarChar)
                }
            };
            var connectionString = configuration.GetConnectionString("db", "dbBinding")
                                    ?? configuration.GetConnectionString("Default");

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Log" }
                , null, null, LogEventLevel.Warning, null, columnOptions: columnOptions, null, null)
                .CreateLogger();

            return services;
        }
    }
}
