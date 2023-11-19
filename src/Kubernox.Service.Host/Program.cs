using FluentValidation;

using Kubernox.Application;
using Kubernox.Application.Features.Identity.Commands;
using Kubernox.Application.Workers;
using Kubernox.Infrastructure;
using Kubernox.Shared.Validators;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.RegisterApplicationLayer(builder.Configuration);
builder.Services.RegisterInfrastructureLayer(builder.Configuration);
builder.Services.AddHostedService<ProxmoxClusterWorker>();
builder.Services.AddValidatorsFromAssemblyContaining<SignInRequestValidator>();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<SignInCommand>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.Authority = builder.Configuration["Identity:Authority"];
    x.Audience = builder.Configuration["Identity:Audience"];
    x.RequireHttpsMetadata = false;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Identity:Issuer"],
        ValidAudience = builder.Configuration["Identity:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Identity:SecurityKey"]))
    };

    x.SaveToken = true;
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MigrateData();
app.MapControllers();

app.Run();