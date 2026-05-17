using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7128");
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IUser, AppUser>();
        }
    }
}
