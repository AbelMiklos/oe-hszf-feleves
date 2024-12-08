using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Model.Exceptions;

namespace GMYEL8_HSZF_2024251.Application.Implementations;

public class JsonFileExportService : IFileExportService
{
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public JsonFileExportService()
	{
		_jsonSerializerOptions = new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
			WriteIndented = true,
		};
	}

	public void ExportData(object dataToExport, string? outputPath = null)
	{
		if (string.IsNullOrWhiteSpace(outputPath))
		{
			outputPath = Path.Combine("./default_statistics_export.json");
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
			throw new BusinessException("No write permission");
		}

		var jsonData = JsonSerializer.Serialize(dataToExport, _jsonSerializerOptions);

		File.WriteAllText(outputPath, jsonData, Encoding.UTF8);
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
