﻿using GMYEL8_HSZF_2024251.Application.Events;
using GMYEL8_HSZF_2024251.Application.Implementations;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;
using Moq;

using NUnit.Framework;

namespace GMYEL8_HSZF_2024251.Test
{
    [TestFixture]
    public class TaxiRouteServiceTests
    {
        private Mock<ITaxiRouteServiceDataProvider> _mockTaxiRouteDataProvider;
        private TaxiRouteService _taxiRouteService;

        [SetUp]
        public void SetUp()
        {
            _mockTaxiRouteDataProvider = new Mock<ITaxiRouteServiceDataProvider>();
            _taxiRouteService = new TaxiRouteService(_mockTaxiRouteDataProvider.Object);
        }

        [Test]
        public void AddTaxiRouteAsync_ShouldThrowException_WhenMaxServicePriceIsZero()
        {
            // Arrange
            var service = new Service { TaxiCarId = "ABC123", PaidAmount = 500 };
            _mockTaxiRouteDataProvider
                .Setup(x => x.GetMaxServicePriceByCarAsync(service.TaxiCarId))
                .ReturnsAsync(0);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _taxiRouteService.AddTaxiRouteAsync(service));
            Assert.That(ex.Message, Is.EqualTo($"Taxi car not found with the given '{service.TaxiCarId}' license plate."));
        }

        [Test]
        public async Task AddTaxiRouteAsync_ShouldAddTaxiRoute_WhenFareIsWithinThreshold()
        {
            // Arrange
            var service = new Service { TaxiCarId = "DEF456", PaidAmount = 500 };
            _mockTaxiRouteDataProvider
                .Setup(x => x.GetMaxServicePriceByCarAsync(service.TaxiCarId))
                .ReturnsAsync(300);

            // Act
            await _taxiRouteService.AddTaxiRouteAsync(service);

            // Assert
            _mockTaxiRouteDataProvider.Verify(x => x.AddTaxiRouteAsync(service), Times.Once);
        }

        [Test]
        public async Task AddTaxiRouteAsync_ShouldRaiseFareExceededEvent_WhenFareExceedsThreshold()
        {
            // Arrange
            var service = new Service { TaxiCarId = "GHI789", PaidAmount = 1000 };
            var maxServicePrice = 400;
            var threshold = maxServicePrice * 2;
            FareExceededEventArgs? capturedEventArgs = null;

            _mockTaxiRouteDataProvider
                .Setup(x => x.GetMaxServicePriceByCarAsync(service.TaxiCarId))
                .ReturnsAsync(maxServicePrice);

            _taxiRouteService.FareExceeded += (sender, e) => capturedEventArgs = e;

            // Act
            await _taxiRouteService.AddTaxiRouteAsync(service);

            // Assert
            Assert.That(capturedEventArgs, Is.Not.Null);
            Assert.That(service.PaidAmount, Is.EqualTo(capturedEventArgs.PaidAmount));
            Assert.That(threshold, Is.EqualTo(capturedEventArgs.Threshold));
        }

        [Test]
        public async Task AddTaxiRouteAsync_ShouldNotRaiseFareExceededEvent_WhenFareIsWithinThreshold()
        {
            // Arrange
            var service = new Service { TaxiCarId = "JKL012", PaidAmount = 500 };
            var maxServicePrice = 300;

            _mockTaxiRouteDataProvider
                .Setup(x => x.GetMaxServicePriceByCarAsync(service.TaxiCarId))
                .ReturnsAsync(maxServicePrice);

            bool eventRaised = false;
            _taxiRouteService.FareExceeded += (sender, e) => eventRaised = true;

            // Act
            await _taxiRouteService.AddTaxiRouteAsync(service);

            // Assert
            Assert.That(eventRaised, Is.False);
        }
    }
}