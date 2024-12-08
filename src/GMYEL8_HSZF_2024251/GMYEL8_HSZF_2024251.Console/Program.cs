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
        var middlewarePipeline = appHost.Services.GetRequiredService<IMiddlewarePipeline>()
            .Use(appHost.Services.GetRequiredService<ICustomMiddleware>());

        return new ConsoleMenuWithMiddleware(middlewarePipeline)
            .Add("Seed data from JSON file", async () => await RunFileReadInteraction(appHost))
            .Add("Data maintain", async () => await RunDataMaintainUserInteraction(appHost, middlewarePipeline))
            .Add("Statistics", async () => await RunStatisticsUserInteraction(appHost, middlewarePipeline))
            .Add("Search routes by car", async () => await RunSearchRoutesByCarInteraction(appHost))
            .Add("Exit", () => Environment.Exit(0));
    }

    private static async Task RunFileReadInteraction(IHost appHost)
    {
        var taxiCarDataSeedService = appHost.Services.GetRequiredService<ITaxiCarDataSeederService>();
        var userInteraction = new FileReadInteraction(taxiCarDataSeedService);

        await userInteraction.ExecuteAsync();
    }

    private static async Task RunDataMaintainUserInteraction(IHost appHost, IMiddlewarePipeline middlewarePipeline)
    {
        var taxiCarCRUDService = appHost.Services.GetRequiredService<ITaxiCarCRUDService>();
        var taxiRouteService = appHost.Services.GetRequiredService<ITaxiRouteService>();

        var dataMaintainInteraction = new DataMaintainInteraction(taxiCarCRUDService, taxiRouteService, middlewarePipeline);

        await dataMaintainInteraction.ExecuteAsync();
    }

    private static async Task RunStatisticsUserInteraction(IHost appHost, IMiddlewarePipeline middlewarePipeline)
    {
        var statisticsService = appHost.Services.GetRequiredService<IStatisticsGeneratorService>();
        var fileExportService = appHost.Services.GetRequiredService<IFileExportService>();

        var statisticsInteraction = new StatisticsInteraction(statisticsService, middlewarePipeline);

        await statisticsInteraction.ExecuteAsync();
    }

    private static async Task RunSearchRoutesByCarInteraction(IHost appHost)
    {
        var taxiCarSearchService = appHost.Services.GetRequiredService<ITaxiCarSearchService>();
        var searchRoutesByCarInteraction = new SearchRoutesByCarInteraction(taxiCarSearchService);

        await searchRoutesByCarInteraction.ExecuteAsync();
    }
}
