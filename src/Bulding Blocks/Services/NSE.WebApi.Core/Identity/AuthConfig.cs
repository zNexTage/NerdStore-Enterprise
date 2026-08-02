using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSE.WebApi.Core.Database;
using NSE.WebApi.Core.Globalization;
using System.Text;

namespace NSE.WebApi.Core.Identity
{
    public class IdentityOptions
    {
        public string Secret { get; set; }
        public int ExpirationTimeInHours { get; set; }
        public string Issuer { get; set; }
        public string ValidIn { get; set; }
    }

    public static class AuthConfig
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<IdentityPortugueseMessages>();

            var authBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            var authSection = configuration.GetSection("AuthenticationSettings");
            services.Configure<IdentityOptions>(authSection);

            var authSettings = authSection.Get<IdentityOptions>();
            var audience = authSettings.ValidIn;
            var issuer = authSettings.Issuer;

            var key = Encoding.ASCII.GetBytes(authSettings.Secret);

            authBuilder.AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = true;
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true, // Valida o token com base na assinatura
                    IssuerSigningKey = new SymmetricSecurityKey(key), // a assinatura é feita através de uma chave
                    ValidateIssuer = true, // Valida o emissor.
                    ValidateAudience = true, // Valida onde esse token é válido?
                    //ValidAudiences = audience, // Define os domínios que o token é aceito                    
                    ValidAudience = audience, // Define o domínio que o token é aceito      
                    ValidIssuer = issuer // Define o emissor.
                };
            });

            return services;
        }
    }
}
