using GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;

using Con = System.Console;

namespace GMYEL8_HSZF_2024251.Console.UserInteractions;

/// <inheritdoc cref="IUserInteraction"/>
public class FileReadInteraction(ITaxiCarDataSeederService taxiCarDataSeederService) : IUserInteraction
{
    private readonly ITaxiCarDataSeederService _taxiCarDataSeederService = taxiCarDataSeederService;

    public async Task ExecuteAsync()
    {
        string filePath = ReadJsonFilePath();

        await _taxiCarDataSeederService.SeedDataAsync(filePath);

        //try
        //{
        //    await _taxiCarDataSeederService.SeedDataAsync(filePath);
        //}
        //catch (Exception)
        //{
        //    Con.WriteLine($"An error occurred while trying to seed data into the database.");
        //}

        Con.WriteLine("Data has been successfully seeded into the database.");
    }

    private string ReadJsonFilePath()
    {
        Con.Write("Please enter the file path of the JSON file to read: ");
        string? filePath = Con.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Con.WriteLine("File path cannot be empty.");

            return ReadJsonFilePath();
        }

        bool isValidFilePath = IsValidFilePath(filePath, ".json");

        if (!isValidFilePath)
        {
            return ReadJsonFilePath();
        }

        return filePath;
    }

    private bool IsValidFilePath(string filePath, string requiredExtension)
    {
        if (!Path.HasExtension(filePath) || Path.GetExtension(filePath).ToLower() != requiredExtension)
        {
            Con.WriteLine("Input file's extension must be \".json\"");

            return false;
        }

        if (!File.Exists(filePath))
        {
            Con.WriteLine("File does not exist.");

            return false;
        }

        try
        {
            using var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        }
        catch (UnauthorizedAccessException)
        {
            Con.WriteLine("Access to the file is denied.");

            return false;
        }
        catch (IOException)
        {
            Con.WriteLine("File is in use by another process.");

            return false;
        }
        catch (Exception)
        {
            Con.WriteLine("An error occurred while trying to access the file.");

            return false;
        }

        return true;
    }
}
