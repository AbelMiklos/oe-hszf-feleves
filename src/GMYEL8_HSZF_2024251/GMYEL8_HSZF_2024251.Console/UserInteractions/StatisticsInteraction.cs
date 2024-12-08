using ConsoleTools;

using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Console.Middleware;

using Con = System.Console;

namespace GMYEL8_HSZF_2024251.Console.UserInteractions;

/// <inheritdoc cref="IUserInteraction"/>
public class StatisticsInteraction(IStatisticsGeneratorService statisticsGeneratorService, IMiddlewarePipeline middlewarePipeline)
    : IUserInteraction
{
    private readonly IStatisticsGeneratorService _statisticsGeneratorService = statisticsGeneratorService;
    private readonly IMiddlewarePipeline _middlewarePipeline = middlewarePipeline;

    public async Task ExecuteAsync()
    {
        var subMenu = new ConsoleMenuWithMiddleware(_middlewarePipeline)
            .Add("Get shorter than 10 km trips per car", GetShorterThan10KmTripsCountPerCarAsync)
            .Add("Get most frequent destination per car", GetMostFrequentDestinationPerCarAsync)
            .Add("Get trip statistics per car", GetTripStatisticsPerCarAsync)
            .Add("Back", ConsoleMenu.Close);

        await subMenu.ShowAsync();
    }

    private async Task GetShorterThan10KmTripsCountPerCarAsync()
    {
        string? outputPath = GetOutputPath("trips less than 10 km");

        await _statisticsGeneratorService.GetShortTripsCountPerCarAsync(outputPath, 10);

        Con.WriteLine("Data exported successfully.");
    }

    private async Task GetMostFrequentDestinationPerCarAsync()
    {
        string? outputPath = GetOutputPath("most frequent destinations per taxi cars");

        await _statisticsGeneratorService.GetMostFrequentDestinationPerCarAsync(outputPath);

        Con.WriteLine("Data exported successfully.");
    }

    private async Task GetTripStatisticsPerCarAsync()
    {
        string? outputPath = GetOutputPath("trip statistics per taxi cars");

        await _statisticsGeneratorService.GetTripStatisticsPerCarAsync(outputPath);

        Con.WriteLine("Data exported successfully.");
    }

    private string? GetOutputPath(string prompt)
    {
        Con.Write($"Please provide the file name where you want to save the data for {prompt} (Hit [Enter] to save to default location): ");
        return Con.ReadLine();
    }
}
