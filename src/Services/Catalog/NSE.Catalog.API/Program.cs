using NSE.Catalog.API.Configuration;

var builder = ApiConfig.CreateBuild(args);

// Add services to the container.
builder
    .Services
    .AddApiConfiguration(builder.Configuration)
    .AddServicesConfig()
    .AddSwaggerConfiguration();

var app = builder.Build();

app
    .UseApiConfiguration(app.Environment)
    .UseSwaggerConfiguration();

app.Run();
