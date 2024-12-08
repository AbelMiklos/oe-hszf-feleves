using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Implementations;

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
				options.UseLazyLoadingProxies(true);
			});

			return services;
		}

		public static IServiceCollection AddMsSqlDataProviders(this IServiceCollection services)
		{
			services.AddSingleton<ITaxiCarServiceDataProvider, TaxiCarDataProvider>();
			services.AddSingleton<ITaxiRouteServiceDataProvider, TaxiRouteDataProvider>();
			services.AddSingleton<IStatisticsServiceDataProvider, StatisticsDataProvider>();

			return services;
		}
	}
}
