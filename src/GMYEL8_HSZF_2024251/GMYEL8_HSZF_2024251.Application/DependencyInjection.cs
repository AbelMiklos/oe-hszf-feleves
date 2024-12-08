using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Application.Definitions.SearchServices;
using GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;
using GMYEL8_HSZF_2024251.Application.Implementations;
using GMYEL8_HSZF_2024251.Application.Implementations.SearchServices;
using GMYEL8_HSZF_2024251.Application.Implementations.TaxiCarServices;

using Microsoft.Extensions.DependencyInjection;

namespace GMYEL8_HSZF_2024251.Application
{
	public static class DependencyInjection
	{
		public static void AddApplicationServices(this IServiceCollection services)
		{
			services.AddSingleton<ITaxiCarDataSeederService, TaxiCarDataSeederService>();
			services.AddSingleton<ITaxiCarCRUDService, TaxiCarCRUDService>();
			services.AddSingleton<ITaxiRouteService, TaxiRouteService>();
			services.AddSingleton<IStatisticsGeneratorService, StatisticsGeneratorService>();
			services.AddSingleton<IFileExportService, JsonFileExportService>();
			services.AddSingleton<ITaxiCarSearchService, TaxiCarSearchService>();
		}
	}
}
