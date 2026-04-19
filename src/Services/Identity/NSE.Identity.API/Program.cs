using NSE.Identity.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
.AddApplicationDbContext()
.AddIdentity();

var app = builder.Build();

app.UseApiConfiguration();

app.Run();
