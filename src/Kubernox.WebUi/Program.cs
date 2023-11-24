using Blazored.LocalStorage;

using Fluxor;

using Kubernox.Shared;
using Kubernox.WebUi;
using Kubernox.WebUi.Core;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FeatureManagement;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAntDesign();
builder.Services.RegisterClientServices(builder.Configuration);
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddFeatureManagement();
builder.Services.AddScoped<AuthenticationStateProvider, KubernoxAuthenticationStateProvider>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost/api/") });

var currentAssembly = typeof(Program).Assembly;
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(currentAssembly);
    options.UseReduxDevTools();
});

builder.Services
    .AddOidcAuthentication(options =>
    {
    });


await builder.Build().RunAsync();