using NSE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.LoadAppSettings();

builder.Services.AddMvcConfig();

builder.Services
    .AddServices()
    .AddHttpServices(builder.Configuration);

var app = builder.Build();

app.UseMvcConfig();

app.Run();
