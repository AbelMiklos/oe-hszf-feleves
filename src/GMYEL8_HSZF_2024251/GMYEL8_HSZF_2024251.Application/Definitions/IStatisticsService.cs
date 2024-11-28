﻿using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.Statistics;

namespace GMYEL8_HSZF_2024251.Application.Definitions;

/// <summary>
///    Provides statistics for <see cref="TaxiCar"/> and <see cref="Service"/> entities.
/// </summary>
public interface IStatisticsService
{
    /// <summary>
    ///     Gets the number of trips less than 10 kilometers for each car.
    /// </summary>
    /// <param name="maxDistance">The maximum distance of a trip to be considered short.</param>
    /// <returns>A dictionary where the key is the <see cref="TaxiCar"/> and the value is the number of trips less than 10 kilometers.</returns>
    Task<Dictionary<TaxiCar, int>> GetShortTripsCountPerCarAsync(int maxDistance);

    /// <summary>
    ///     Gets the average trip distance, the longest trip, and the shortest trip for each car.
    /// </summary>
    /// <returns>A dictionary where the key is the <see cref="TaxiCar"/> and the value is a <see cref="TaxiCarServiceStatistic"/> containing the average trip distance, the longest trip, and the shortest trip.</returns>
    Task<Dictionary<TaxiCar, TaxiCarServiceStatistic>> GetTripStatisticsPerCarAsync();

    /// <summary>
    ///    Gets the most frequent destination for each car.
    /// </summary>
    /// <returns>A dictionary where the key is the <see cref="TaxiCar"/> and the value is the name of the most frequent destination.</returns>
    Task<Dictionary<TaxiCar, string>> GetMostFrequentDestinationPerCarAsync();    
}