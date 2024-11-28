using GMYEL8_HSZF_2024251.Application.Implementations.TaxiCarServices;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

using Moq;

using NUnit.Framework;

namespace GMYEL8_HSZF_2024251.Test
{
    [TestFixture]
    public class TaxiCarServiceTester
    {
        private  TaxiCarCRUDService _taxiCarCRUDService;
        private Mock<ITaxiCarServiceDataProvider> _mockTaxiCarDataProvider;

        [SetUp]
        public void Setup()
        {
            _mockTaxiCarDataProvider = new Mock<ITaxiCarServiceDataProvider>();
            _mockTaxiCarDataProvider.Setup(m => m.GetTaxiCarByIdAsync("ABC123")).Returns(Task.FromResult(new TaxiCar
            {
                LicensePlate = "ABC123",
                Driver = "John Doe"
            })!);
            _mockTaxiCarDataProvider.Setup(m => m.GetTaxiCarByIdAsync("DEF456")).Returns(Task.FromResult<TaxiCar?>(null));

            _taxiCarCRUDService = new TaxiCarCRUDService(_mockTaxiCarDataProvider.Object);
        }
    }
}
