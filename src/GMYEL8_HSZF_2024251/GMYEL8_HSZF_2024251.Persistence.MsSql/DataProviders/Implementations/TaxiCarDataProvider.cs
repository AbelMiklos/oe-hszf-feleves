using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

using Microsoft.EntityFrameworkCore;

namespace GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Implementations;

/// <inheritdoc cref="ITaxiCarServiceDataProvider"/>
public class TaxiCarDataProvider(AppDbContext context) : ITaxiCarServiceDataProvider
{
	private readonly AppDbContext _context = context;

	public IQueryable<TaxiCar> ReadAll()
	{
		return _context.TaxiCars
			.Include(t => t.Services)
			.Select(t => t);
	}

	public async Task<TaxiCar?> GetTaxiCarByIdAsync(string licencePlate)
	{
		return await ReadAll()
			.FirstOrDefaultAsync(taxi => taxi.LicensePlate == licencePlate);
	}

	public void AddTaxiCar(TaxiCar taxiCar)
	{
		_context.TaxiCars.Add(taxiCar);
	}

	public void UpdateTaxiCar(TaxiCar taxiCar)
	{
		_context.TaxiCars.Update(taxiCar);
	}

	public void DeleteTaxiCar(TaxiCar taxiToDelete)
	{
		_context.TaxiCars.Remove(taxiToDelete);
	}

	public void AddServiceToTaxiCar(Service service)
	{
		_context.Services.Add(service);
	}

	public async Task<bool> IsTaxiCarsExistsAsync(string licencePlate)
	{
		return await _context.TaxiCars.AnyAsync(taxi => taxi.LicensePlate == licencePlate);
	}

	public async Task SaveChangesAsync()
	{
		await _context.SaveChangesAsync();
	}
}
