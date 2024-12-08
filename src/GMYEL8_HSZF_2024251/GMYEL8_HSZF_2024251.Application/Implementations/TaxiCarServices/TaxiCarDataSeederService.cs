using System.Text;
using System.Text.Json;

using GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.Exceptions;
using GMYEL8_HSZF_2024251.Model.JsonWrappers;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

namespace GMYEL8_HSZF_2024251.Application.Implementations.TaxiCarServices;

/// <inheritdoc cref="ITaxiCarDataSeederService"/>
public class TaxiCarDataSeederService(ITaxiCarServiceDataProvider taxiCarServiceDataProvider) : ITaxiCarDataSeederService
{
	private readonly ITaxiCarServiceDataProvider _taxiCarServiceDataProvider = taxiCarServiceDataProvider;

	public async Task SeedDataAsync(string filePath)
	{
		CheckFileExists(filePath);
		CheckFileExtension(filePath);

		string fileContent = await ReadJsonAsync(filePath);
		var taxiCars = GetTaxiCarsFromJson(fileContent);

		await AddTaxiCarsToDb(taxiCars);
	}

	private static void CheckFileExists(string filePath)
	{
		if (!File.Exists(filePath))
		{
			string errorMessage = $"The file at the given path '{filePath}' does not exist.";
			throw new BusinessException(errorMessage, new FileNotFoundException(errorMessage));
		}
	}

	private static void CheckFileExtension(string filePath)
	{
		if (Path.GetExtension(filePath) != ".json")
		{
			string errorMessage = "The file must be in JSON format.";
			throw new BusinessException(errorMessage, new ArgumentException(errorMessage));
		}
	}

	private static async Task<string> ReadJsonAsync(string filePath)
	{
		try
		{
			return await File.ReadAllTextAsync(filePath, Encoding.UTF8);
		}
		catch (Exception ex)
		{
			string errorMessage = $"Failed to read the file at '{filePath}'.";
			throw new BusinessException(errorMessage, new IOException(errorMessage, ex));
		}
	}

	private static List<TaxiCar> GetTaxiCarsFromJson(string fileContent)
	{
		List<TaxiCar>? taxiCars;
		try
		{
			taxiCars = JsonSerializer.Deserialize<TaxiCarWrapper>(fileContent, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			})?.TaxiCars;
		}
		catch (JsonException ex)
		{
			string errorMessage = "The file content is not a valid JSON.";
			throw new BusinessException(errorMessage, new InvalidDataException(errorMessage, ex));
		}

		if (taxiCars is null
			|| taxiCars.Count == 0)
		{
			string errorMessage = "The file does not contain any valid taxi car data.";
			throw new BusinessException(errorMessage, new InvalidDataException(errorMessage));
		}

		return taxiCars;
	}

	private async Task AddTaxiCarsToDb(List<TaxiCar> taxiCars)
	{
		foreach (var taxiCar in taxiCars)
		{
			var existingTaxiCar = await _taxiCarServiceDataProvider.GetTaxiCarByIdAsync(taxiCar.LicensePlate);

			if (existingTaxiCar is null)
			{
				_taxiCarServiceDataProvider.AddTaxiCar(taxiCar);
			}
			else
			{
				foreach (var service in taxiCar.Services)
				{
					var newService = new Service
					{
						Distance = service.Distance,
						FareStartDate = service.FareStartDate,
						From = service.From,
						PaidAmount = service.PaidAmount,
						TaxiCarId = existingTaxiCar.LicensePlate,
						To = service.To
					};

					_taxiCarServiceDataProvider.AddServiceToTaxiCar(newService);
				}
			}
		}

		await _taxiCarServiceDataProvider.SaveChangesAsync();
	}
}
