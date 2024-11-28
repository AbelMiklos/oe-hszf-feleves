using GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

namespace GMYEL8_HSZF_2024251.Application.Implementations.TaxiCarServices;

/// <inheritdoc cref="ITaxiCarCRUDService"/>
public class TaxiCarCRUDService(ITaxiCarServiceDataProvider dataProvider) : ITaxiCarCRUDService
{
    private readonly ITaxiCarServiceDataProvider _dataProvider = dataProvider;

    public async Task CreateTaxiCarAsync(TaxiCar newTaxiCar)
    {
        var existingTaxiCar = await _dataProvider.GetTaxiCarByIdAsync(newTaxiCar.LicensePlate);

        if (existingTaxiCar is not null)
        {
            throw new ArgumentException($"Taxi car with the given {newTaxiCar.LicensePlate} already exists.");
        }

        await _dataProvider.AddTaxiCarAsync(newTaxiCar);
    }

    public async Task DeleteTaxiCarAsync(TaxiCar taxiCar)
    {
        var existingTaxiCar = await _dataProvider.GetTaxiCarByIdAsync(taxiCar.LicensePlate)
            ?? throw new ArgumentException($"Taxi car with the given {taxiCar.LicensePlate} does not exists.");

        await _dataProvider.DeleteTaxiCarAsync(taxiCar);
    }

    public async Task<TaxiCar> GetTaxiCarByIdAsync(string licencePlate)
    {
        var taxiCar = await _dataProvider.GetTaxiCarByIdAsync(licencePlate)
            ?? throw new ArgumentException($"Taxi car with the given {licencePlate} does not exists.");

        return taxiCar;
    }

    public async Task UpdateTaxiCarAsync(TaxiCar updatedTaxiCar)
    {
        var taxiCarToModify = await _dataProvider.GetTaxiCarByIdAsync(updatedTaxiCar.LicensePlate)
            ?? throw new ArgumentException($"Taxi car with the given {updatedTaxiCar.LicensePlate} does not exists.");

        taxiCarToModify = updatedTaxiCar;

        await _dataProvider.UpdateTaxiCarAsync(taxiCarToModify);
    }
}
