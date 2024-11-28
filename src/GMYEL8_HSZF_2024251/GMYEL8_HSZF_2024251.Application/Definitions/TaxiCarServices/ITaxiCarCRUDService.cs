using GMYEL8_HSZF_2024251.Model.Entities;

namespace GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;

/// <summary>
///    CRUD operations for TaxiCar entities.
/// </summary>
public interface ITaxiCarCRUDService
{
    /// <summary>
    ///     Creates a new TaxiCar record in the database.
    /// </summary>
    /// <param name="newTaxiCar">The TaxiCar entity to be created.</param>
    Task CreateTaxiCarAsync(TaxiCar newTaxiCar);

    /// <summary>
    ///     Retrieves a TaxiCar record by its license plate.
    /// </summary>
    /// <param name="licencePlate">The license plate of the TaxiCar to retrieve.</param>
    /// <returns>TaxiCar entity with the given <paramref name="licencePlate"/>.</returns>
    Task<TaxiCar> GetTaxiCarByIdAsync(string licencePlate);

    /// <summary>
    ///     Updates an existing TaxiCar record in the database.
    /// </summary>
    /// <param name="updatedTaxiCar">The TaxiCar entity with updated information.</param>
    Task UpdateTaxiCarAsync(TaxiCar updatedTaxiCar);

    /// <summary>
    ///     Deletes a TaxiCar record from the database.
    /// </summary>
    /// <param name="taxiCar">The TaxiCar entity to be deleted.</param>
    Task DeleteTaxiCarAsync(TaxiCar taxiCar);
}
