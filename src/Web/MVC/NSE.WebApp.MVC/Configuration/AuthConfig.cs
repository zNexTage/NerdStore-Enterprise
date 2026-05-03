using Microsoft.AspNetCore.Authentication.Cookies;

namespace NSE.WebApp.MVC.Configuration
{
    public static class AuthConfig
    {
        public static void AddAuthConfig(this IServiceCollection services) {
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.LoginPath = "/login";
                    opts.AccessDeniedPath = "/acesso-negado";                    
                });
        }

        public static void UseAuthConfig(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();           
        }
    }
}
