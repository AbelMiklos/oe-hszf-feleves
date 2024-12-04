using ConsoleTools;

using GMYEL8_HSZF_2024251.Application.Definitions;

using Con = System.Console;

namespace GMYEL8_HSZF_2024251.Console.UserInteractions;

public class StatisticsInteraction(IStatisticsGeneratorService statisticsGeneratorService, IFileExportService fileExportService) : IUserInteraction
{
    private readonly IStatisticsGeneratorService _statisticsGeneratorService = statisticsGeneratorService;

    public async Task ExecuteAsync()
    {
        var subMenu = new ConsoleMenu()
            .Add("Get shorter than 10 km trips per car", async () => await GetShorterThan10KmTripsCountPerCarAsync())
            .Add("Get most frequent destination per car", async () => await GetMostFrequentDestinationPerCarAsync())
            .Add("Get trip statistics per car", async () => await GetTripStatisticsPerCarAsync())
            .Add("Back", ConsoleMenu.Close);

        await subMenu.ShowAsync();
    }

    private async Task GetShorterThan10KmTripsCountPerCarAsync()
    {
        string? outputPath = GetOutputPath("trips less than 10 km");

        try
        {
            await _statisticsGeneratorService.GetShortTripsCountPerCarAsync(outputPath, 10);

            Con.WriteLine("Data exported successfully.");
        }
        catch (Exception ex)
        {
            Con.WriteLine($"Error during export: {ex.Message}");
        }
    }

    private async Task GetMostFrequentDestinationPerCarAsync()
    {
        string? outputPath = GetOutputPath("most frequent destinations per taxi cars");

        try
        {
            await _statisticsGeneratorService.GetMostFrequentDestinationPerCarAsync(outputPath);

            Con.WriteLine("Data exported successfully.");
        }
        catch (Exception ex)
        {
            Con.WriteLine($"Error during export: {ex.Message}");
        }
    }

    private async Task GetTripStatisticsPerCarAsync()
    {
        string? outputPath = GetOutputPath("trip statistics per taxi cars");

        await _statisticsGeneratorService.GetTripStatisticsPerCarAsync(outputPath);

        Con.WriteLine("Data exported successfully.");

        //try
        //{
        //    await _statisticsGeneratorService.GetTripStatisticsPerCarAsync(outputPath);

        //    Con.WriteLine("Data exported successfully.");
        //}
        //catch (Exception ex)
        //{
        //    Con.WriteLine($"Error during export: {ex.Message}");
        //}
    }

    private string? GetOutputPath(string prompt)
    {
        Con.Write($"Please provide the file name where you want to save the data for {prompt} (Hit [Enter] to save to default location): ");
        return Con.ReadLine();
    }
}
