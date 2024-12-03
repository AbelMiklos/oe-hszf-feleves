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

    public async Task<PaginatedResult<TaxiCar>> SearchTaxiCarsAsync(TaxiCarSearchCriteria criteria)
    {
        var query = _taxiCarDataProvider.ReadAll();

        if (!string.IsNullOrEmpty(criteria.LicensePlate))
        {
            query = query.Where(t => t.LicensePlate == criteria.LicensePlate);
        }

        if (!string.IsNullOrEmpty(criteria.Driver))
        {
            query = query.Where(t => t.Driver == criteria.Driver);
        }

        int totalCount = query.Count();

        var items = query
            .Skip((criteria.PageNumber - 1) * criteria.PageSize)
            .Take(criteria.PageSize)
            .ToList();

        var result = new PaginatedResult<TaxiCar>
        {
            Items = items,
            TotalCount = totalCount,
            CurrentPage = criteria.PageNumber,
            PageSize = criteria.PageSize
        };

        return await Task.FromResult(result);
    }
}
