using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NSE.Identity.API.Data;
using NSE.Identity.API.Extensions;
using System.Text;

namespace NSE.Identity.API.Configuration
{
    public class IdentityOptions
    {
        public string Secret { get; set; }
        public int ExpirationTimeInHours { get; set; }
        public string Issuer { get; set; }
        public string ValidIn { get; set; }
    }

    public static class IdentityConfiguration
    {
        public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<IdentityPortugueseMessages>();

            var authBuilder = builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            var authSection = builder.Configuration.GetSection("AuthenticationSettings");
            builder.Services.Configure<IdentityOptions>(authSection);

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

            return builder;
        }
    }
}
