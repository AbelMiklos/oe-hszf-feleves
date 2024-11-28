using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.Statistics;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

namespace GMYEL8_HSZF_2024251.Application.Implementations;

/// <inheritdoc cref="IStatisticsService"/>
public class StatisticsService(IStatisticsServiceDataProvider dataProvider) : IStatisticsService
{
    private readonly IStatisticsServiceDataProvider _dataProvider = dataProvider;

    public async Task<Dictionary<TaxiCar, int>> GetShortTripsCountPerCarAsync(int maxDistance = 10)
    {
        return await _dataProvider.GetAggregatedDataAsync<int>(
            groupByExpression: trip => trip.TaxiCar,
            aggregationExpression: groups => groups
                .Select(g => new
                {
                    Car = g.Key,
                    Count = g.Count(service => service.Distance < maxDistance)
                })
                .Select(result => new KeyValuePair<TaxiCar, int>(result.Car, result.Count)));
    }

    public async Task<Dictionary<TaxiCar, string>> GetMostFrequentDestinationPerCarAsync()
    {
        return await _dataProvider.GetAggregatedDataAsync<string>(
            groupByExpression: trip => trip.TaxiCar,
            aggregationExpression: groups => groups
                .Select(g => new
                {
                    Car = g.Key,
                    MostFrequentDestination = g.GroupBy(t => t.To)
                                               .OrderByDescending(destGroup => destGroup.Count())
                                               .Select(destGroup => destGroup.Key)
                                               .FirstOrDefault()
                })
                .Select(result => new KeyValuePair<TaxiCar, string>(result.Car, result.MostFrequentDestination ?? string.Empty)));
    }

    public async Task<Dictionary<TaxiCar, TaxiCarServiceStatistic>> GetTripStatisticsPerCarAsync()
    {
        return await _dataProvider.GetAggregatedDataAsync<TaxiCarServiceStatistic>(
            groupByExpression: trip => trip.TaxiCar,
            aggregationExpression: groups => groups
                .Select(g => new TaxiCarServiceStatistic
                {
                    Car = g.Key,
                    AverageDistance = g.Average(service => service.Distance),
                    LongestTrip = g.OrderByDescending(service => service.Distance).FirstOrDefault(),
                    ShortestTrip = g.OrderBy(service => service.Distance).FirstOrDefault()
                })
                .Select(result => new KeyValuePair<TaxiCar, TaxiCarServiceStatistic>(
                    result.Car,
                    result)));
    }
}
