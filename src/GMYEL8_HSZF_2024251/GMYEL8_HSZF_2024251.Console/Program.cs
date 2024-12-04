using ConsoleTools;

using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Application.Definitions.SearchServices;
using GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;
using GMYEL8_HSZF_2024251.Console.Middleware;
using GMYEL8_HSZF_2024251.Console.UserInteractions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GMYEL8_HSZF_2024251.Console;

public class Program
{
    public static async Task Main()
    {
        var configuration = AppStart.ReadConfiguration();

        var host = AppStart.CreateAppHost(configuration);
        await host.StartAsync();
        
        var mainMenu = CreateMenu(host);
        await mainMenu.ShowAsync();
    }

    private static ConsoleMenu CreateMenu(IHost appHost)
    {
        var middlewarePipeline = appHost.Services.GetRequiredService<MiddlewarePipeline>();
        middlewarePipeline
            .Use(appHost.Services.GetRequiredService<ICustomMiddleware>());

        return new ConsoleMenu()
            .Add("Seed data from JSON file", async () => await middlewarePipeline.ExecuteAsync(async () =>
            {
                var taxiCarDataSeedService = appHost.Services.GetRequiredService<ITaxiCarDataSeederService>();
                var userInteraction = new FileReadInteraction(taxiCarDataSeedService);

                await userInteraction.ExecuteAsync();
            }))
            .Add("Data maintain", async () => await middlewarePipeline.ExecuteAsync(async () =>
            {
                var taxiCarCRUDService = appHost.Services.GetRequiredService<ITaxiCarCRUDService>();
                var taxiRouteService = appHost.Services.GetRequiredService<ITaxiRouteService>();

                var dataMaintainInteraction = new DataMaintainInteraction(taxiCarCRUDService, taxiRouteService);

                await dataMaintainInteraction.ExecuteAsync();
            }))
            .Add("Statistics", async () => await middlewarePipeline.ExecuteAsync(async () =>
            {
                var statisticsService = appHost.Services.GetRequiredService<IStatisticsGeneratorService>();
                var fileExportService = appHost.Services.GetRequiredService<IFileExportService>();

                var statisticsInteraction = new StatisticsInteraction(statisticsService, fileExportService);

                await statisticsInteraction.ExecuteAsync();
            }))
            .Add("Search routes by car", async () => await middlewarePipeline.ExecuteAsync(async () =>
            {
                var taxiCarSearchService = appHost.Services.GetRequiredService<ITaxiCarSearchService>();
                var searchRoutesByCarInteraction = new SearchRoutesByCarInteraction(taxiCarSearchService);

                await searchRoutesByCarInteraction.ExecuteAsync();
            }))
            .Add("Exit", () => Environment.Exit(0));
    }
}
