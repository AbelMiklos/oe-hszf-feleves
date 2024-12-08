using ConsoleTools;

using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Console.Middleware;

using Con = System.Console;

namespace GMYEL8_HSZF_2024251.Console.UserInteractions;

/// <inheritdoc cref="IUserInteraction"/>
public class StatisticsInteraction(
    IStatisticsGeneratorService statisticsGeneratorService,
    IFileExportService fileExportService,
    IMiddlewarePipeline middlewarePipeline): IUserInteraction
{
    private readonly IStatisticsGeneratorService _statisticsGeneratorService = statisticsGeneratorService;
    private readonly IFileExportService _fileExportService = fileExportService;

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

        var shortTripsPerCar = await _statisticsGeneratorService.GetShortTripsCountPerCarAsync(10);
        _fileExportService.ExportData(shortTripsPerCar, outputPath);

        ExportedSuccessfully();
    }

    private async Task GetMostFrequentDestinationPerCarAsync()
    {
        string? outputPath = GetOutputPath("most frequent destinations per taxi cars");

        var mostFrequentDestinations = await _statisticsGeneratorService.GetMostFrequentDestinationPerCarAsync();
        _fileExportService.ExportData(mostFrequentDestinations, outputPath);

        ExportedSuccessfully();
    }

    private async Task GetTripStatisticsPerCarAsync()
    {
        string? outputPath = GetOutputPath("trip statistics per taxi cars");

        var tripStatisticsPerCar = await _statisticsGeneratorService.GetTripStatisticsPerCarAsync();
        _fileExportService.ExportData(tripStatisticsPerCar, outputPath);

        ExportedSuccessfully();
    }

    private string? GetOutputPath(string prompt)
    {
        Con.Write($"Please provide the file name where you want to save the data for {prompt} (Hit [Enter] to save to default location): ");
        return Con.ReadLine();
    }

    private void ExportedSuccessfully()
    {
        Con.WriteLine("Data exported successfully.");
        Con.WriteLine("Press any key to return to the menu...");
        Con.ReadKey();
    }
}
