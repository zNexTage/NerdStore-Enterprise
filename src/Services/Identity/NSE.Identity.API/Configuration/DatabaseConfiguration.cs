using Microsoft.EntityFrameworkCore;
using NSE.Identity.API.Data;

namespace NSE.Identity.API.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
