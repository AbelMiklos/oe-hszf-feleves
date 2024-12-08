namespace GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;

/// <summary>
///     Seeding TaxiCar data into the database.
/// </summary>
public interface ITaxiCarDataSeederService
{
    /// <summary>
    ///     Seeds the database with TaxiCar entities from the given JSON file.
    /// </summary>
    /// <param name="filePath">Location of the source file with the seed data.</param>
    Task SeedDataAsync(string filePath);
}
