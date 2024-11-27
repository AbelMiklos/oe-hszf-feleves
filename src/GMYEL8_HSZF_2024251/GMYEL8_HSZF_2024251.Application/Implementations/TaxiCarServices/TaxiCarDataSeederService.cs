using GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.JsonWrappers;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

using System.Text;
using System.Text.Json;

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
            throw new FileNotFoundException($"The file at the given path '{filePath}' does not exist.");
        }
    }

    private static void CheckFileExtension(string filePath)
    {
        if (Path.GetExtension(filePath) != ".json")
        {
            throw new ArgumentException("The file must be in JSON format.");
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
            throw new IOException($"Failed to read the file at '{filePath}'.", ex);
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
            throw new InvalidDataException("The file content is not a valid JSON.", ex);
        }

        if (taxiCars is null
            || taxiCars.Count == 0)
        {
            throw new InvalidDataException("The file does not contain any valid taxi car data.");
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
                await _taxiCarServiceDataProvider.AddTaxiCarAsync(taxiCar);
            }
            else
            {
                foreach (var service in taxiCar.Services)
                {
                    existingTaxiCar.Services.Add(service);
                }

                await _taxiCarServiceDataProvider.UpdateTaxiCarAsync(existingTaxiCar);
            }
        }
    }
}
