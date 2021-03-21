using Fluxor;
using Kubernox.UI.Layout;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kubernox.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            //builder.RootComponents.Add<App>("#app");

            ConfigureCore(builder);

            ConfigureLocalization(builder);

            ConfigureDependencyInjection(builder);

            await builder.Build().RunAsync();
        }

        private static void ConfigureCore(WebAssemblyHostBuilder builder)
        {

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}")
            });
            MainLayout.BaseUri = builder.HostEnvironment.BaseAddress;

            builder.Services.AddAntDesign();
            var currentAssembly = typeof(StoreRegistration).Assembly;
            builder.Services.AddFluxor(options => options.ScanAssemblies(currentAssembly));
        }

        private static void ConfigureLocalization(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddScoped<IStringLocalizer<App>, StringLocalizer<App>>();
        }

        private static void ConfigureDependencyInjection(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<IClusterService, ClusterService>();
            builder.Services.AddScoped<IDatacenterService, DatacenterService>();
            builder.Services.AddScoped<ITemplateService, TemplateService>();
            builder.Services.AddScoped<ISshKeyService, SshKeyService>();
        }
    }
}
