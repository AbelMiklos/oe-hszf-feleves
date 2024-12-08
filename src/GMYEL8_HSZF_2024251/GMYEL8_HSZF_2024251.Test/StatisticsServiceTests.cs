using GMYEL8_HSZF_2024251.Application.Implementations;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.JsonWrappers;
using GMYEL8_HSZF_2024251.Model.Statistics;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

using Moq;

using NUnit.Framework;

namespace GMYEL8_HSZF_2024251.Test;

[TestFixture]
public class StatisticsGeneratorServiceTests
{
	private Mock<IStatisticsServiceDataProvider> _mockDataProvider;
	private StatisticsGeneratorService _service;

	[SetUp]
	public void SetUp()
	{
		_mockDataProvider = new Mock<IStatisticsServiceDataProvider>();
		_service = new StatisticsGeneratorService(_mockDataProvider.Object);
	}

	[Test]
	public async Task GetShortTripsCountPerCarAsync_ShouldReturnCorrectCounts()
	{
		// Arrange
		var maxDistance = 10;
		var taxiCar = new TaxiCar { LicensePlate = "ABC123" };
		var expectedData = new Dictionary<TaxiCar, int> { { taxiCar, 2 } };

		_mockDataProvider
			.Setup(x => x.GetShortTripsCountPerCarAsync(maxDistance))
			.ReturnsAsync(expectedData);

		var expected = expectedData.Select(pair => new TaxiCarWithTripsCount
		{
			TaxiCar = pair.Key,
			TripsCount = pair.Value
		});

		// Act
		var result = await _service.GetShortTripsCountPerCarAsync(maxDistance);

		// Assert
		Assert.That(result, Is.EquivalentTo(expected).Using<TaxiCarWithTripsCount>((x, y) =>
			Equals(x.TaxiCar, y.TaxiCar) && x.TripsCount == y.TripsCount));
	}

	[Test]
	public async Task GetMostFrequentDestinationPerCarAsync_ShouldReturnCorrectDestinations()
	{
		// Arrange
		var taxiCar = new TaxiCar { LicensePlate = "DEF456" };
		var expectedData = new Dictionary<TaxiCar, string> { { taxiCar, "Airport" } };

		_mockDataProvider
			.Setup(x => x.GetMostFrequentDestinationPerCarAsync())
			.ReturnsAsync(expectedData);

		var expected = expectedData.Select(pair => new TaxiCarMostFrequentDestination
		{
			TaxiCar = pair.Key,
			MostFrequentDestination = pair.Value
		}).ToList();

		// Act
		var result = (await _service.GetMostFrequentDestinationPerCarAsync()).ToList();

		// Assert
		Assert.That(result, Is.EquivalentTo(expected).Using<TaxiCarMostFrequentDestination>((x, y) =>
			Equals(x.TaxiCar, y.TaxiCar) && x.MostFrequentDestination == y.MostFrequentDestination));
	}

	[Test]
	public async Task GetTripStatisticsPerCarAsync_ShouldReturnCorrectStatistics()
	{
		// Arrange
		var taxiCar = new TaxiCar { LicensePlate = "GHI789" };
		var longestTrip = new Service { TaxiCar = taxiCar, Distance = 30 };
		var shortestTrip = new Service { TaxiCar = taxiCar, Distance = 5 };
		var expectedStats = new TaxiCarServiceStatistic
		{
			TaxiCar = taxiCar,
			AverageDistance = 15,
			LongestTrip = longestTrip,
			ShortestTrip = shortestTrip
		};
		var expectedData = new Dictionary<TaxiCar, TaxiCarServiceStatistic>
		{
			{ taxiCar, expectedStats }
		};

		_mockDataProvider
			.Setup(x => x.GetTripStatisticsPerCarAsync())
			.ReturnsAsync(expectedData);

		var expected = expectedData.Select(pair => new TaxiCarServiceStatistic
		{
			TaxiCar = pair.Key,
			AverageDistance = pair.Value.AverageDistance,
			LongestTrip = pair.Value.LongestTrip,
			ShortestTrip = pair.Value.ShortestTrip
		}).ToList();

		// Act
		var result = (await _service.GetTripStatisticsPerCarAsync()).ToList();

		// Assert
		Assert.That(result, Is.EquivalentTo(expected).Using<TaxiCarServiceStatistic>((x, y) =>
			Equals(x.TaxiCar, y.TaxiCar)
			&& x.AverageDistance == y.AverageDistance
			&& Equals(x.LongestTrip, y.LongestTrip)
			&& Equals(x.ShortestTrip, y.ShortestTrip)));
	}
}
