//using GMYEL8_HSZF_2024251.Application.Implementations;
//using GMYEL8_HSZF_2024251.Model.Entities;
//using GMYEL8_HSZF_2024251.Model.Statistics;
//using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

//using Moq;

//using NUnit.Framework;

//namespace GMYEL8_HSZF_2024251.Test
//{
//    [TestFixture]
//    public class StatisticsServiceTests
//    {
//        private Mock<IStatisticsServiceDataProvider> _mockDataProvider;
//        private StatisticsService _service;

//        [SetUp]
//        public void SetUp()
//        {
//            _mockDataProvider = new Mock<IStatisticsServiceDataProvider>();
//            _service = new StatisticsService(_mockDataProvider.Object);
//        }

//        [Test]
//        public async Task GetShortTripsCountPerCarAsync_ShouldReturnCorrectCounts()
//        {
//            // Arrange
//            var maxDistance = 10;
//            var taxiCar = new TaxiCar { LicensePlate = "ABC123" };
//            var expected = new Dictionary<TaxiCar, int> { { taxiCar, 2 } };

//            _mockDataProvider
//                .Setup(x => x.GetShortTripsCountPerCarAsync(maxDistance))
//                .ReturnsAsync(expected);

//            // Act
//            var result = await _service.GetShortTripsCountPerCarAsync(TODO, maxDistance);

//            // Assert
//            Assert.That(result, Is.EqualTo(expected));
//        }

//        [Test]
//        public async Task GetMostFrequentDestinationPerCarAsync_ShouldReturnCorrectDestinations()
//        {
//            // Arrange
//            var taxiCar = new TaxiCar { LicensePlate = "DEF456" };
//            var expected = new Dictionary<TaxiCar, string> { { taxiCar, "Airport" } };

//            _mockDataProvider
//                .Setup(x => x.GetMostFrequentDestinationPerCarAsync())
//                .ReturnsAsync(expected);

//            // Act
//            var result = await _service.GetMostFrequentDestinationPerCarAsync(TODO);

//            // Assert
//            Assert.That(result, Is.EqualTo(expected));
//        }

//        [Test]
//        public async Task GetTripStatisticsPerCarAsync_ShouldReturnCorrectStatistics()
//        {
//            // Arrange
//            var taxiCar = new TaxiCar { LicensePlate = "GHI789" };
//            var longestTrip = new Service { TaxiCar = taxiCar, Distance = 30 };
//            var shortestTrip = new Service { TaxiCar = taxiCar, Distance = 5 };
//            var expectedStats = new TaxiCarServiceStatistic
//            {
//                TaxiCar = taxiCar,
//                AverageDistance = 15,
//                LongestTrip = longestTrip,
//                ShortestTrip = shortestTrip
//            };
//            var expected = new Dictionary<TaxiCar, TaxiCarServiceStatistic>
//        {
//            { taxiCar, expectedStats }
//        };

//            _mockDataProvider
//                .Setup(x => x.GetTripStatisticsPerCarAsync())
//                .ReturnsAsync(expected);

//            // Act
//            var result = await _service.GetTripStatisticsPerCarAsync();

//            // Assert
//            Assert.That(result, Is.EqualTo(expected));
//            Assert.That(result[taxiCar].AverageDistance, Is.EqualTo(expectedStats.AverageDistance));
//            Assert.That(result[taxiCar].LongestTrip, Is.EqualTo(expectedStats.LongestTrip));
//            Assert.That(result[taxiCar].ShortestTrip, Is.EqualTo(expectedStats.ShortestTrip));
//        }
//    }
//}
