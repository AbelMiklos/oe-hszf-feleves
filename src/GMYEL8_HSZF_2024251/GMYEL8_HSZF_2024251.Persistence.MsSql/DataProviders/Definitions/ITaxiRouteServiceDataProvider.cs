using GMYEL8_HSZF_2024251.Model.Entities;

namespace GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

/// <summary>
///     Provides data access methods for taxi fare services.
/// </summary>
public interface ITaxiRouteServiceDataProvider
{
	/// <summary>
	///     
	/// </summary>
	/// <param name="licensePlate"></param>
	/// <returns></returns>
	Task<int> GetMaxServicePriceByCarAsync(string licensePlate);

	/// <summary>
	///    Adds a new taxi service to the database.
	/// </summary>
	/// <param name="taxiRoute"></param>
	Task AddTaxiRouteAsync(Service taxiService);
}
