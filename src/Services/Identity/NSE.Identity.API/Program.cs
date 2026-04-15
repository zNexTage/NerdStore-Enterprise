using NSE.Identity.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration();

var app = builder.Build();

app.UseApiConfiguration();

app.Run();
