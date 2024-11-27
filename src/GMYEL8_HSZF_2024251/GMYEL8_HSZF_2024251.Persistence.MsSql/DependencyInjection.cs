using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GMYEL8_HSZF_2024251.Persistence.MsSql
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMsSqlDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseLazyLoadingProxies();
            });

            return services;
        }
    }
}
