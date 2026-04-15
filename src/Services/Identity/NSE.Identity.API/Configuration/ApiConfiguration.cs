namespace NSE.Identity.API.Configuration
{
    public static class ApiConfiguration
    {
        public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddApplicationDbContext(builder.Configuration["DefaultConnection"])
                .AddIdentity()
                .AddControllers(); // Retorna um IMvcBuilder, logo não da para chamar o AddOpenApi

            builder.Services.AddOpenApi();

            return builder;
        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection()
            .UseAuthentication()
            .UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
