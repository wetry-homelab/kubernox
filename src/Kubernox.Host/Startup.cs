using Fluxor;
using Kubernox.UI;
using Kubernox.UI.Layout;
using Kubernox.UI.Services;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Services;
using System;
using System.Linq;
using System.Net.Http;

namespace Kubernox.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddRazorPages();

            ConfigureCore(services);
            ConfigureLocalization(services);
            ConfigureDependencyInjection(services);
        }

        private static void ConfigureCore(IServiceCollection services)
        {
            services.AddSingleton<HttpClient>(sp =>
            {
                var server = sp.GetRequiredService<IServer>();
                var addressFeature = server.Features.Get<IServerAddressesFeature>();
                string baseAddress = addressFeature.Addresses.First();
                MainLayout.BaseUri = $"{baseAddress}";
                return new HttpClient { BaseAddress = new Uri(baseAddress) };
            });

            services.AddAntDesign();
            var currentAssembly = typeof(StoreRegistration).Assembly;
            services.AddFluxor(options => options.ScanAssemblies(currentAssembly));
        }

        private static void ConfigureLocalization(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddScoped<IStringLocalizer<App>, StringLocalizer<App>>();
        }

        private static void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IClusterService, ClusterService>();
            services.AddScoped<IDatacenterService, DatacenterService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<ISshKeyService, SshKeyService>();
            services.AddScoped<IDomainNameService, DomainNameService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("index.html");

                //endpoints.MapRazorPages(); // <- Add this
                //endpoints.MapFallbackToPage("index.html"); // <- Change method + file
            });
        }
    }
}

