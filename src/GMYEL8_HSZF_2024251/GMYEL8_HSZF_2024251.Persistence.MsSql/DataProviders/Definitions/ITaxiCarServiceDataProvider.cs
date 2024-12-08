using GMYEL8_HSZF_2024251.Model.Entities;

namespace GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

public interface ITaxiCarServiceDataProvider
{
	/// <summary>
	///    Retrieves all TaxiCar entities from the database.
	/// </summary>
	/// <returns>Taxi car entities.</returns>
	IQueryable<TaxiCar> ReadAll();

	/// <summary>
	///     Retrieves a TaxiCar entity by its unique identifier.
	/// </summary>
	/// <param name="licencePlate">The unique identifier of the TaxiCar.</param>
	/// <returns>The TaxiCar entity if found; otherwise, null.</returns>
	Task<TaxiCar?> GetTaxiCarByIdAsync(string licencePlate);

	/// <summary>
	///    Adds the given TaxiCar entity to the database.
	/// </summary>
	/// <param name="taxiCar">TaxiCar entity to be added.</param>
	void AddTaxiCar(TaxiCar taxiCar);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="taxiCar"></param>
	/// <returns></returns>
	void UpdateTaxiCar(TaxiCar taxiCar);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="taxiToDelete"></param>
	/// <returns></returns>
	void DeleteTaxiCar(TaxiCar taxiToDelete);

	/// <summary>
	///     Adds a service to a TaxiCar.
	/// </summary>
	/// <param name="service">The service to be added to the TaxiCar.</param>
	void AddServiceToTaxiCar(Service service);

	/// <summary>
	///    Retrieves if the TaxiCar exists in the database.
	/// </summary>
	/// <param name="licencePlate"></param>
	Task<bool> IsTaxiCarsExistsAsync(string licencePlate);

	/// <summary>
	///     Saves all changes made in this context to the database asynchronously.
	/// </summary>
	Task SaveChangesAsync();
}
