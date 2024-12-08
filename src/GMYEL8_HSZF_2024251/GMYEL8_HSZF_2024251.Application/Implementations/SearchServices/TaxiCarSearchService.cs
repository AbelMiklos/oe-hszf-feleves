using GMYEL8_HSZF_2024251.Application.Definitions.SearchServices;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.Search.Criterias;
using GMYEL8_HSZF_2024251.Model.Search.Results;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

namespace GMYEL8_HSZF_2024251.Application.Implementations.SearchServices;

/// <inheritdoc cref="ITaxiCarSearchService"/>
public class TaxiCarSearchService(ITaxiCarServiceDataProvider taxiCarDataProvider) : ITaxiCarSearchService
{
	private readonly ITaxiCarServiceDataProvider _taxiCarDataProvider = taxiCarDataProvider;

	public async Task<PaginatedResult<Service>> SearchTaxiCarsAsync(TaxiCarSearchCriteria criteria)
	{
		var taxiCars = _taxiCarDataProvider.ReadAll();

		if (!string.IsNullOrEmpty(criteria.LicensePlate))
		{
			taxiCars = taxiCars.Where(t => t.LicensePlate.Contains(criteria.LicensePlate));
		}

		if (!string.IsNullOrEmpty(criteria.Driver))
		{
			taxiCars = taxiCars.Where(t => t.Driver.Contains(criteria.Driver));
		}

		var taxiCarServices = taxiCars
			.SelectMany(t => t.Services)
			.AsQueryable();

		int totalCount = taxiCarServices.Count();

		var paginatedServices = taxiCarServices
			.Skip((criteria.PageNumber - 1) * criteria.PageSize)
			.Take(criteria.PageSize)
			.ToList();

		var result = new PaginatedResult<Service>
		{
			Items = paginatedServices,
			TotalCount = totalCount,
			CurrentPage = criteria.PageNumber,
			PageSize = criteria.PageSize
		};

		return await Task.FromResult(result);
	}
}
