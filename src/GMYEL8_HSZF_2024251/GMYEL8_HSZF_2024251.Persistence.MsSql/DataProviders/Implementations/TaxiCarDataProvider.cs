using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

using Microsoft.EntityFrameworkCore;

namespace GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Implementations;

/// <inheritdoc cref="ITaxiCarServiceDataProvider"/>
public class TaxiCarDataProvider(AppDbContext context) : ITaxiCarServiceDataProvider
{
    private readonly AppDbContext _context = context;

    public async Task<TaxiCar?> GetTaxiCarByIdAsync(string licencePlate)
    {
        return await _context.TaxiCars
            .Include(taxi => taxi.Services)
            .FirstOrDefaultAsync(taxi => taxi.LicensePlate == licencePlate);
    }

    public async Task AddTaxiCarAsync(TaxiCar taxiCar)
    {
        _context.TaxiCars.Add(taxiCar);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaxiCarAsync(TaxiCar taxiCar)
    {
        _context.TaxiCars.Update(taxiCar);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaxiCarAsync(TaxiCar taxiToDelete)
    {
        _context.TaxiCars.Remove(taxiToDelete);
        await _context.SaveChangesAsync();
    }
}
