using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Model.JsonWrappers;
using GMYEL8_HSZF_2024251.Model.Statistics;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

namespace GMYEL8_HSZF_2024251.Application.Implementations;

/// <inheritdoc cref="IStatisticsGeneratorService"/>
public class StatisticsGeneratorService(IStatisticsServiceDataProvider dataProvider, IFileExportService fileExportService) : IStatisticsGeneratorService
{
    private readonly IStatisticsServiceDataProvider _dataProvider = dataProvider;

    private readonly IFileExportService _fileExportService = fileExportService;

    public async Task GetShortTripsCountPerCarAsync(string? outpuPath, int maxDistance = 10)
    {
        var statistics = await _dataProvider.GetShortTripsCountPerCarAsync(maxDistance);

        var tripsCountPerCar = statistics
            .Select(pair => new TaxiCarWithTripsCount
            {
                TaxiCar = pair.Key,
                TripsCount = pair.Value
            });

        ExportData(tripsCountPerCar, outpuPath);
    }

    public async Task GetMostFrequentDestinationPerCarAsync(string? outputPath)
    {
        var statistics = await _dataProvider.GetMostFrequentDestinationPerCarAsync();

        var frequentDestinationsPerCar = statistics
            .Select(pair => new TaxiCarMostFrequentDestination
            {
                TaxiCar = pair.Key,
                MostFrequentDestination = pair.Value
            });

        ExportData(frequentDestinationsPerCar, outputPath);
    }

    public async Task GetTripStatisticsPerCarAsync(string? outputPath)
    {
        var statistics = await _dataProvider.GetTripStatisticsPerCarAsync();

        var tripStatisticsPerCar = statistics
            .Select(pair => new TaxiCarServiceStatistic
            {
                TaxiCar = pair.Key,
                AverageDistance = pair.Value.AverageDistance,
                LongestTrip = pair.Value.LongestTrip,
                ShortestTrip = pair.Value.ShortestTrip
            });

        ExportData(tripStatisticsPerCar, outputPath);
    }

    private void ExportData<T>(IEnumerable<T> data, string? outputPath)
    {
        _fileExportService.ExportData(data, outputPath);
    }
}
