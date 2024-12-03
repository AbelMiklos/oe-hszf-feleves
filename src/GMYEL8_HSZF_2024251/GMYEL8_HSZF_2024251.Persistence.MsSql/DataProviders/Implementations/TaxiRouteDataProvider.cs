using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

using Microsoft.EntityFrameworkCore;

namespace GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Implementations;

/// <inheritdoc cref="ITaxiRouteServiceDataProvider"/>
public class TaxiRouteDataProvider(AppDbContext context) : ITaxiRouteServiceDataProvider
{
    private readonly AppDbContext _context = context;

    public async Task<int> GetMaxServicePriceByCarAsync(string licensePlate)
    {
        var services = await _context.Services
            .Where(service => service.TaxiCar.LicensePlate == licensePlate)
            .ToListAsync();

        return services.DefaultIfEmpty(new Service { PaidAmount = 0 })
            .Max(service => service.PaidAmount);
    }

    public async Task AddTaxiRouteAsync(Service taxiService)
    {
        _context.Services.Add(taxiService);
        await _context.SaveChangesAsync();
    }

}
