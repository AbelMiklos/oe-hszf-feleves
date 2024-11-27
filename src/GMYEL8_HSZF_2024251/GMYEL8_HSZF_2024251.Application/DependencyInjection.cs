using GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;
using GMYEL8_HSZF_2024251.Application.Implementations.TaxiCarServices;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Implementations;

using Microsoft.Extensions.DependencyInjection;

namespace GMYEL8_HSZF_2024251.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ITaxiCarServiceDataProvider, TaxiCarDataProvider>();
            services.AddSingleton<ITaxiCarDataSeederService, TaxiCarDataSeederService>();
            services.AddSingleton<ITaxiCarCRUDService, TaxiCarCRUDService>();
        }
    }
}
