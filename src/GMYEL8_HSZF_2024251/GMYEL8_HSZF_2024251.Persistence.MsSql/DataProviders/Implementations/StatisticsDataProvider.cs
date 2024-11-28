using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Implementations;

/// <inheritdoc cref="IStatisticsServiceDataProvider"/>
public class StatisticsDataProvider(AppDbContext context) : IStatisticsServiceDataProvider
{
    private readonly AppDbContext _context = context;

    public async Task<Dictionary<TaxiCar, TResult>> GetAggregatedDataAsync<TResult>(
        Expression<Func<Service, TaxiCar>> groupByExpression,
        Expression<Func<IQueryable<IGrouping<TaxiCar, Service>>, IQueryable<KeyValuePair<TaxiCar, TResult>>>> aggregationExpression)
    {
        var query = aggregationExpression.Compile()(
            _context.Services
                .Include(trip => trip.TaxiCar)
                .AsQueryable()
                .GroupBy(groupByExpression));

        return (await query.ToListAsync())
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}
