using GMYEL8_HSZF_2024251.Application.Implementations;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.Statistics;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

using Moq;

using NUnit.Framework;

using System.Linq.Expressions;

namespace GMYEL8_HSZF_2024251.Test
{
    [TestFixture]
    public class StatisticsServiceTests
    {
        private Mock<IStatisticsServiceDataProvider> _mockDataProvider;
        private StatisticsService _service;

        [SetUp]
        public void SetUp()
        {
            _mockDataProvider = new Mock<IStatisticsServiceDataProvider>();
            _service = new StatisticsService(_mockDataProvider.Object);
        }

        [Test]
        public async Task GetShortTripsCountPerCarAsync_ShouldReturnCorrectCounts()
        {
            // Arrange
            var maxDistance = 10;
            var taxiCar = new TaxiCar { LicensePlate = "ABC123" };
            var trips = new List<Service>
            {
                new Service { TaxiCar = taxiCar, Distance = 5 },
                new Service { TaxiCar = taxiCar, Distance = 15 },
                new Service { TaxiCar = taxiCar, Distance = 8 }
            };
            var expected = new Dictionary<TaxiCar, int> { { taxiCar, 2 } };

            _mockDataProvider
                .Setup(x => x.GetAggregatedDataAsync<int>(
                    It.IsAny<Expression<Func<Service, TaxiCar>>>(),
                    It.IsAny<Expression<Func<IQueryable<IGrouping<TaxiCar, Service>>, IQueryable<KeyValuePair<TaxiCar, int>>>>>()
                ))
                .ReturnsAsync(expected);

            // Act
            var result = await _service.GetShortTripsCountPerCarAsync(maxDistance);

            // Assert
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public async Task GetMostFrequentDestinationPerCarAsync_ShouldReturnCorrectDestinations()
        {
            // Arrange
            var taxiCar = new TaxiCar { LicensePlate = "DEF456" };
            var expected = new Dictionary<TaxiCar, string> { { taxiCar, "Airport" } };

            _mockDataProvider
                .Setup(x => x.GetAggregatedDataAsync<string>(
                    It.IsAny<Expression<Func<Service, TaxiCar>>>(),
                    It.IsAny<Expression<Func<IQueryable<IGrouping<TaxiCar, Service>>, IQueryable<KeyValuePair<TaxiCar, string>>>>>()
                ))
                .ReturnsAsync(expected);

            // Act
            var result = await _service.GetMostFrequentDestinationPerCarAsync();

            // Assert
            Assert.That(expected, Is.EqualTo(result));
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
                Car = taxiCar,
                AverageDistance = 15,
                LongestTrip = longestTrip,
                ShortestTrip = shortestTrip
            };
            var expected = new Dictionary<TaxiCar, TaxiCarServiceStatistic>
            {
                { taxiCar, expectedStats }
            };

            _mockDataProvider
                .Setup(x => x.GetAggregatedDataAsync<TaxiCarServiceStatistic>(
                    It.IsAny<Expression<Func<Service, TaxiCar>>>(),
                    It.IsAny<Expression<Func<IQueryable<IGrouping<TaxiCar, Service>>, IQueryable<KeyValuePair<TaxiCar, TaxiCarServiceStatistic>>>>>()
                ))
                .ReturnsAsync(expected);

            // Act
            var result = await _service.GetTripStatisticsPerCarAsync();

            // Assert
            Assert.That(expected, Is.EqualTo(result));
            Assert.That(expectedStats.AverageDistance, Is.EqualTo(result[taxiCar].AverageDistance));
            Assert.That(expectedStats.LongestTrip, Is.EqualTo(result[taxiCar].LongestTrip));
            Assert.That(expectedStats.ShortestTrip, Is.EqualTo(result[taxiCar].ShortestTrip));
        }
    }
}
