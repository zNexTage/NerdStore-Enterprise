namespace NSE.Identity.API.Configuration
{
    public static class ApiConfiguration
    {
        public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            var connectionString = builder.Configuration["DefaultConnection"];

            builder.Services
                .AddApplicationDbContext(connectionString)
                .AddIdentity()
                .AddControllers(); // Retorna um IMvcBuilder, logo não da para chamar o AddOpenApi

            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "NerdStore Enterprise Identity API",
                    Description = "Esta API faz parte do curso ASP.NET Core Enterprise Applications",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Email = "oliveirachristian1908@gmail.com", Name = "Christian Bueno"},
                    License = new Microsoft.OpenApi.Models.OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });

            return builder;
        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }

            app.UseHttpsRedirection()
            .UseAuthentication()
            .UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
