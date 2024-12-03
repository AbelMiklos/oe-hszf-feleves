using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.Statistics;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

using Microsoft.EntityFrameworkCore;

namespace GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Implementations;

/// <inheritdoc cref="IStatisticsServiceDataProvider"/>
public class StatisticsDataProvider(AppDbContext context) : IStatisticsServiceDataProvider
{
    private readonly AppDbContext _context = context;

    public async Task<Dictionary<TaxiCar, int>> GetShortTripsCountPerCarAsync(int maxDistance)
    {
        var query = await _context.Services
            .Include(service => service.TaxiCar)
            .Where(service => service.Distance < maxDistance)
            .GroupBy(service => service.TaxiCar)
            .Select(group => new KeyValuePair<TaxiCar, int>(group.Key, group.Count()))
            .ToListAsync();

        return query.ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public async Task<Dictionary<TaxiCar, string>> GetMostFrequentDestinationPerCarAsync()
    {
        var query = await _context.Services
            .Include(service => service.TaxiCar)
            .GroupBy(service => service.TaxiCar)
            .Select(group => new KeyValuePair<TaxiCar, string>(
                group.Key,
                group.GroupBy(service => service.To)
                     .OrderByDescending(destGroup => destGroup.Count())
                     .Select(destGroup => destGroup.Key)
                     .FirstOrDefault() ?? string.Empty))
            .ToListAsync();

        return query.ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public async Task<Dictionary<TaxiCar, TaxiCarServiceStatistic>> GetTripStatisticsPerCarAsync()
    {
        var query = await _context.Services
            .Include(service => service.TaxiCar)
            .GroupBy(service => service.TaxiCar)
            .Select(group => new KeyValuePair<TaxiCar, TaxiCarServiceStatistic>(
                group.Key,
                new TaxiCarServiceStatistic
                {
                    TaxiCar = group.Key,
                    AverageDistance = group.Average(service => service.Distance),
                    LongestTrip = group.OrderByDescending(service => service.Distance).FirstOrDefault(),
                    ShortestTrip = group.OrderBy(service => service.Distance).FirstOrDefault()
                }))
            .ToListAsync();

        return query.ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}
