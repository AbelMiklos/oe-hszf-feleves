using ConsoleTools;

using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Model.JsonWrappers;

using Con = System.Console;

namespace GMYEL8_HSZF_2024251.Console.UserInteractions;

public class StatisticsInteraction(IStatisticsService statisticsService, IFileExportService fileExportService) : IUserInteraction
{
    private readonly IStatisticsService _statisticsService = statisticsService;
    private readonly IFileExportService _fileExportService = fileExportService;

    public async Task ExecuteAsync()
    {
        var subMenu = new ConsoleMenu()
            .Add("Get shorter than 10 km trips per car", async () => await GetShorterThan10KmTripsCountPerCarAsync())
            .Add("Back", ConsoleMenu.Close);

        await subMenu.ShowAsync();
    }

    private async Task GetShorterThan10KmTripsCountPerCarAsync()
    {
        var shortTrips = await _statisticsService.GetShortTripsCountPerCarAsync(10);

        var tripsData = shortTrips
            .Select(pair => new TaxiCarWithTripsCount
            {
                TaxiCar = pair.Key,
                TripsCount = pair.Value
            })
            .ToList();

        Con.WriteLine("Please provide the file name where you want to save the data for trips less than 10 km (Press [Enter] to save to default location:");
        string outputPath = Con.ReadLine();

        try
        {
            _fileExportService.ExportData(tripsData, outputPath);
        }
        catch (Exception ex)
        {
            Con.WriteLine($"Error during export: {ex.Message}");
        }

        Con.WriteLine("Data exported successfully.");
    }
}
