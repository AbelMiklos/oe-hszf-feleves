using GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.Exceptions;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

namespace GMYEL8_HSZF_2024251.Application.Implementations.TaxiCarServices;

/// <inheritdoc cref="ITaxiCarCRUDService"/>
public class TaxiCarCRUDService(ITaxiCarServiceDataProvider dataProvider) : ITaxiCarCRUDService
{
    const string TaxiCarAlreadyExistsErrorMessage = "Taxi car with the given license plate {0} already exists.";
    const string TaxiCarNotExistsErrorMessage = "Taxi car with the given license plate {0} does not exists.";

    private readonly ITaxiCarServiceDataProvider _dataProvider = dataProvider;

    public async Task CreateTaxiCarAsync(TaxiCar newTaxiCar)
    {
        var existingTaxiCar = await _dataProvider.GetTaxiCarByIdAsync(newTaxiCar.LicensePlate);

        if (existingTaxiCar is not null)
        {
            string errorMessage = string.Format(TaxiCarAlreadyExistsErrorMessage, newTaxiCar.LicensePlate);
            throw new BusinessException(errorMessage, new ArgumentException(errorMessage));
        }

        _dataProvider.AddTaxiCar(newTaxiCar);
        await _dataProvider.SaveChangesAsync();
    }

    public async Task DeleteTaxiCarAsync(TaxiCar taxiCar)
    {
        string errorMessage = string.Format(TaxiCarNotExistsErrorMessage, taxiCar.LicensePlate);
        var existingTaxiCar = await _dataProvider.GetTaxiCarByIdAsync(taxiCar.LicensePlate)
            ?? throw new BusinessException(errorMessage, new ArgumentException(errorMessage));

        _dataProvider.DeleteTaxiCar(taxiCar);
        await _dataProvider.SaveChangesAsync();
    }

    public async Task<TaxiCar> GetTaxiCarByIdAsync(string licencePlate)
    {
        string errorMessage = string.Format(TaxiCarNotExistsErrorMessage, licencePlate);
        var taxiCar = await _dataProvider.GetTaxiCarByIdAsync(licencePlate)
            ?? throw new BusinessException(errorMessage, new ArgumentException(errorMessage));

        return taxiCar;
    }

    public async Task UpdateTaxiCarAsync(TaxiCar updatedTaxiCar)
    {
        bool isTaxiCarExists = await _dataProvider.IsTaxiCarsExistsAsync(updatedTaxiCar.LicensePlate);

        if (!isTaxiCarExists)
        {
            string errorMessage = string.Format(TaxiCarNotExistsErrorMessage, updatedTaxiCar.LicensePlate);
            throw new BusinessException(errorMessage, new ArgumentException(errorMessage));
        }

        _dataProvider.UpdateTaxiCar(updatedTaxiCar);
        await _dataProvider.SaveChangesAsync();
    }
}
