using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddAuthttpClientService(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44386");
            });
        }
    }
}
