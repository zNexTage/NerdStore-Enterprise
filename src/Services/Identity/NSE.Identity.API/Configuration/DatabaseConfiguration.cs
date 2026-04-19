using Microsoft.EntityFrameworkCore;
using NSE.Identity.API.Data;

namespace NSE.Identity.API.Configuration
{
    public static class DatabaseConfiguration
    {
        public static WebApplicationBuilder AddApplicationDbContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration["DefaultConnection"];

            builder.Services
                .AddDbContext<ApplicationDbContext>(opts =>
                {
                    opts.UseSqlServer(connectionString);
                });

            return builder;
        }
    }
}
