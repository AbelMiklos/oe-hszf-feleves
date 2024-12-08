using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Model.JsonWrappers;
using GMYEL8_HSZF_2024251.Model.Statistics;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

namespace GMYEL8_HSZF_2024251.Application.Implementations;

/// <inheritdoc cref="IStatisticsGeneratorService"/>
public class StatisticsGeneratorService(IStatisticsServiceDataProvider dataProvider) : IStatisticsGeneratorService
{
	private readonly IStatisticsServiceDataProvider _dataProvider = dataProvider;

	public async Task<IEnumerable<TaxiCarWithTripsCount>> GetShortTripsCountPerCarAsync(int maxDistance = 10)
	{
		var statistics = await _dataProvider.GetShortTripsCountPerCarAsync(maxDistance);

		var tripsCountPerCar = statistics
			.Select(pair => new TaxiCarWithTripsCount
			{
				TaxiCar = pair.Key,
				TripsCount = pair.Value
			});

		return tripsCountPerCar;
	}

	public async Task<IEnumerable<TaxiCarMostFrequentDestination>> GetMostFrequentDestinationPerCarAsync()
	{
		var statistics = await _dataProvider.GetMostFrequentDestinationPerCarAsync();

		var frequentDestinationsPerCar = statistics
			.Select(pair => new TaxiCarMostFrequentDestination
			{
				TaxiCar = pair.Key,
				MostFrequentDestination = pair.Value
			});

		return frequentDestinationsPerCar;
	}

	public async Task<IEnumerable<TaxiCarServiceStatistic>> GetTripStatisticsPerCarAsync()
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

		return tripStatisticsPerCar;
	}
}
