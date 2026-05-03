using NSE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcConfig();

var app = builder.Build();

app.UseMvcConfig();

app.Run();
