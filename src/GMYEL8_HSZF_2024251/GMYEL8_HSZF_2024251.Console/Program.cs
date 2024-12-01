using ConsoleTools;

using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;
using GMYEL8_HSZF_2024251.Console.UserInteractions;

using Microsoft.Extensions.DependencyInjection;

namespace GMYEL8_HSZF_2024251.Console;

public class Program
{
    public static async Task Main()
    {
        var configuration = AppStart.ReadConfiguration();

        var host = AppStart.CreateAppHost(configuration);
        await host.StartAsync();

        var menu = new ConsoleMenu()
            .Add("Seed data from JSON file", async () =>
            {
                var taxiCarDataSeedService = host.Services.GetRequiredService<ITaxiCarDataSeederService>();
                var userInteraction = new FileReadInteraction(taxiCarDataSeedService);

                await userInteraction.ExecuteAsync();
            })
            .Add("Data maintain", async () =>
            {
                var taxiCarCRUDService = host.Services.GetRequiredService<ITaxiCarCRUDService>();
                var taxiRouteService = host.Services.GetRequiredService<ITaxiRouteService>();

                var dataMaintainInteraction = new DataMaintainInteraction(taxiCarCRUDService, taxiRouteService);

                await dataMaintainInteraction.ExecuteAsync();
            })
            .Add("Statistics", async () =>
            {
                var statisticsService = host.Services.GetRequiredService<IStatisticsService>();
                var fileExportService = host.Services.GetRequiredService<IFileExportService>();

                var statisticsInteraction = new StatisticsInteraction(statisticsService, fileExportService);

                await statisticsInteraction.ExecuteAsync();
            })
            .Add("Exit", () => Environment.Exit(0));

        await menu.ShowAsync();
    }
}
