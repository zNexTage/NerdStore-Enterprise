using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebApi.Core.Database;

namespace NSE.WebApi.Core.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DefaultConnection"];

            services
                .AddDbContext<ApplicationDbContext>(opts =>
                {
                    opts.UseSqlServer(connectionString);
                });

            return services;
        }
    }
}
