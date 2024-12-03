using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.Statistics;

namespace GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

/// <summary>
///     Provides data for the statistics service.
/// </summary>
public interface IStatisticsServiceDataProvider
{
    /// <summary>
    ///     Gets the count of short trips per car.
    /// </summary>
    /// <param name="maxDistance">The maximum distance to consider a trip as short.</param>
    /// <returns>A dictionary where the key is the TaxiCar and the value is the count of short trips.</returns>
    Task<Dictionary<TaxiCar, int>> GetShortTripsCountPerCarAsync(int maxDistance);

    /// <summary>
    ///     Gets the most frequent destination per car.
    /// </summary>
    /// <returns>A dictionary where the key is the TaxiCar and the value is the most frequent destination.</returns>
    Task<Dictionary<TaxiCar, string>> GetMostFrequentDestinationPerCarAsync();

    /// <summary>
    ///     Gets the trip statistics per car.
    /// </summary>
    /// <returns>A dictionary where the key is the TaxiCar and the value is the trip statistics.</returns>
    Task<Dictionary<TaxiCar, TaxiCarServiceStatistic>> GetTripStatisticsPerCarAsync();
}
