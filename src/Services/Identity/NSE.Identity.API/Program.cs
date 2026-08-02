using NSE.Identity.API.Configuration;
using NSE.WebApi.Core.Configuration;
using NSE.WebApi.Core.Identity;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddApiConfiguration();

var configuration = builder.Configuration;

builder
    .Services
    .AddJwtConfiguration(configuration)
    .AddApplicationDbContext(configuration);

var app = builder.Build();

app.UseApiConfiguration();

app.Run();
