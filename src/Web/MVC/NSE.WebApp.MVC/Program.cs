using NSE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcConfig();

builder.Services.AddAuthttpClientService();

var app = builder.Build();

app.UseMvcConfig();

app.Run();
