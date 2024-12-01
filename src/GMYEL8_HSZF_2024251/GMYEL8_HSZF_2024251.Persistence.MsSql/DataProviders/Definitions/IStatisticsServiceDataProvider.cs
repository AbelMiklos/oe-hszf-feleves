using GMYEL8_HSZF_2024251.Model.Entities;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

/// <summary>
///     Provides data for the statistics service.
/// </summary>
public interface IStatisticsServiceDataProvider
{
    Task<Dictionary<TaxiCar, TResult>> GetAggregatedDataAsync<TResult>(
        Expression<Func<Service, TaxiCar>> groupByExpression,
        Expression<Func<IQueryable<IGrouping<TaxiCar, Service>>, IQueryable<KeyValuePair<TaxiCar, TResult>>>> aggregationExpression);
}
