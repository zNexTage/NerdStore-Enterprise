using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Options;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder LoadAppSettings(this WebApplicationBuilder builder)
        {
            var current = Directory.GetCurrentDirectory();

            builder.Configuration.SetBasePath(current) // Or env.ContentRootPath
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true) //
                .AddEnvironmentVariables();

            return builder;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUser, AppUser>();

            return services;
        }

        public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
        {
            var httpServiceOptions = configuration
                .GetSection(nameof(HttpServiceOptions))
                .Get<HttpServiceOptions>();

            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri(httpServiceOptions.AuthService.BaseAddress);
            });

            return services;
        }
    }
}
