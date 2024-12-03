using GMYEL8_HSZF_2024251.Application.Implementations.TaxiCarServices;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

using Moq;

using NUnit.Framework;

namespace GMYEL8_HSZF_2024251.Test
{
    [TestFixture]
    public class TaxiCarCRUDServiceTests
    {
        private Mock<ITaxiCarServiceDataProvider> _mockDataProvider;
        private TaxiCarCRUDService _service;

        [SetUp]
        public void SetUp()
        {
            _mockDataProvider = new Mock<ITaxiCarServiceDataProvider>();
            _service = new TaxiCarCRUDService(_mockDataProvider.Object);
        }

        [Test]
        public async Task CreateTaxiCarAsync_ShouldThrowException_WhenTaxiCarAlreadyExists()
        {
            // Arrange
            var taxiCar = new TaxiCar { LicensePlate = "ABC123" };
            _mockDataProvider
                .Setup(x => x.GetTaxiCarByIdAsync(taxiCar.LicensePlate))
                .ReturnsAsync(taxiCar);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _service.CreateTaxiCarAsync(taxiCar));
            Assert.That(ex.Message, Is.EqualTo($"Taxi car with the given license plate {taxiCar.LicensePlate} already exists."));
        }

        [Test]
        public async Task CreateTaxiCarAsync_ShouldAddTaxiCar_WhenTaxiCarDoesNotExist()
        {
            // Arrange
            var taxiCar = new TaxiCar { LicensePlate = "DEF456" };
            _mockDataProvider
                .Setup(x => x.GetTaxiCarByIdAsync(taxiCar.LicensePlate))
                .ReturnsAsync((TaxiCar)null);

            // Act
            await _service.CreateTaxiCarAsync(taxiCar);

            // Assert
            _mockDataProvider.Verify(x => x.AddTaxiCar(taxiCar), Times.Once);
        }

        [Test]
        public async Task DeleteTaxiCarAsync_ShouldThrowException_WhenTaxiCarDoesNotExist()
        {
            // Arrange
            var taxiCar = new TaxiCar { LicensePlate = "GHI789" };
            _mockDataProvider
                .Setup(x => x.GetTaxiCarByIdAsync(taxiCar.LicensePlate))
                .ReturnsAsync((TaxiCar)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteTaxiCarAsync(taxiCar));
            Assert.That(ex.Message, Is.EqualTo($"Taxi car with the given license plate {taxiCar.LicensePlate} does not exists."));
        }

        [Test]
        public async Task DeleteTaxiCarAsync_ShouldDeleteTaxiCar_WhenTaxiCarExists()
        {
            // Arrange
            var taxiCar = new TaxiCar { LicensePlate = "JKL012" };
            _mockDataProvider
                .Setup(x => x.GetTaxiCarByIdAsync(taxiCar.LicensePlate))
                .ReturnsAsync(taxiCar);

            // Act
            await _service.DeleteTaxiCarAsync(taxiCar);

            // Assert
            _mockDataProvider.Verify(x => x.DeleteTaxiCar(taxiCar), Times.Once);
        }

        [Test]
        public async Task GetTaxiCarByIdAsync_ShouldThrowException_WhenTaxiCarDoesNotExist()
        {
            // Arrange
            var licensePlate = "MNO345";
            _mockDataProvider
                .Setup(x => x.GetTaxiCarByIdAsync(licensePlate))
                .ReturnsAsync((TaxiCar)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _service.GetTaxiCarByIdAsync(licensePlate));
            Assert.That(ex.Message, Is.EqualTo($"Taxi car with the given license plate {licensePlate} does not exists."));
        }

        [Test]
        public async Task GetTaxiCarByIdAsync_ShouldReturnTaxiCar_WhenTaxiCarExists()
        {
            // Arrange
            var taxiCar = new TaxiCar { LicensePlate = "PQR678" };
            _mockDataProvider
                .Setup(x => x.GetTaxiCarByIdAsync(taxiCar.LicensePlate))
                .ReturnsAsync(taxiCar);

            // Act
            var result = await _service.GetTaxiCarByIdAsync(taxiCar.LicensePlate);

            // Assert
            Assert.That(taxiCar, Is.EqualTo(result));
        }

        [Test]
        public async Task UpdateTaxiCarAsync_ShouldThrowException_WhenTaxiCarDoesNotExist()
        {
            // Arrange
            var updatedTaxiCar = new TaxiCar { LicensePlate = "STU901" };
            _mockDataProvider
                .Setup(x => x.GetTaxiCarByIdAsync(updatedTaxiCar.LicensePlate))
                .ReturnsAsync((TaxiCar)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateTaxiCarAsync(updatedTaxiCar));
            Assert.That(ex.Message, Is.EqualTo($"Taxi car with the given {updatedTaxiCar.LicensePlate} does not exists."));
        }

        [Test]
        public async Task UpdateTaxiCarAsync_ShouldUpdateTaxiCar_WhenTaxiCarExists()
        {
            // Arrange
            var existingTaxiCar = new TaxiCar { LicensePlate = "VWX234" };
            var updatedTaxiCar = new TaxiCar { LicensePlate = "VWX234" };

            _mockDataProvider
                .Setup(x => x.GetTaxiCarByIdAsync(existingTaxiCar.LicensePlate))
                .ReturnsAsync(existingTaxiCar);

            _mockDataProvider
                .Setup(x => x.IsTaxiCarsExistsAsync(existingTaxiCar.LicensePlate))
                .ReturnsAsync(true);

            _mockDataProvider
                .Setup(x => x.UpdateTaxiCar(It.IsAny<TaxiCar>()))
                .Verifiable();

            // Act
            await _service.UpdateTaxiCarAsync(updatedTaxiCar);

            // Assert
            _mockDataProvider.Verify(x => x.UpdateTaxiCar(updatedTaxiCar), Times.Once);
        }
    }
}
