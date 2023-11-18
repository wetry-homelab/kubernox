using Kubernox.Application.Workers;
using Kubernox.Infrastructure;
using Kubernox.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.RegisterApplicationLayer(builder.Configuration);
builder.Services.RegisterInfrastructureLayer(builder.Configuration);

builder.Services.AddHostedService<ProxmoxClusterWorker>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MigrateData();

app.Run();
