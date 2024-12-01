using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Model.JsonWrappers;

using System.Text.Json;

namespace GMYEL8_HSZF_2024251.Application.Implementations;

public class JsonFileExportService : IFileExportService
{
    //public void ExportData(TaxiCarWrapper dataToExport, string? outputPath = null)
    public void ExportData(object dataToExport, string? outputPath = null)
    {
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "default_statistics_export.json");
        }

        var directory = Path.GetDirectoryName(outputPath);
        if (directory != null && !Directory.Exists(directory))
        {
            try
            {
                Directory.CreateDirectory(directory);
            }
            catch (Exception)
            {
                throw new Exception("Error creating directory");
            }
        }

        if (!HasWritePermission(directory))
        {
            throw new Exception("No write permission");
        }

        var jsonData = JsonSerializer.Serialize(dataToExport, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(outputPath, jsonData);
    }

    private bool HasWritePermission(string? directory)
    {
        if (string.IsNullOrEmpty(directory))
            return false;

        try
        {
            var testFile = Path.Combine(directory, "test.json");
            File.WriteAllText(testFile, "test");
            File.Delete(testFile);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
