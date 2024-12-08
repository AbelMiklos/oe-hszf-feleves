using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.Search.Criterias;
using GMYEL8_HSZF_2024251.Model.Search.Results;

namespace GMYEL8_HSZF_2024251.Application.Definitions.SearchServices;

/// <summary>
///     Service for searching TaxiCar entities.
/// </summary>
public interface ITaxiCarSearchService
{
	/// <summary>
	///    Searches TaxiCar entities based on the provided criteria.
	/// </summary>
	/// <param name="criteria">The criteria to filter the TaxiCar entities.</param>
	/// <returns>A paginated list of TaxiCar entities that match the search criteria.</returns>
	Task<PaginatedResult<Service>> SearchTaxiCarsAsync(TaxiCarSearchCriteria criteria);
}
