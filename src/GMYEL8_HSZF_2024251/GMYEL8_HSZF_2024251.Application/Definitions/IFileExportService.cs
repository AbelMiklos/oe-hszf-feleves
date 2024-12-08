namespace GMYEL8_HSZF_2024251.Application.Definitions;

/// <summary>
///    Represents a service that can export data to a file.
/// </summary>
public interface IFileExportService
{
	/// <summary>
	///     Exports the provided data to the specified output path.
	/// </summary>
	/// <param name="dataToExport">The data to be exported.</param>
	/// <param name="outputPath">The path where the exported data will be saved. If null, a default path will be used.</param>
	void ExportData(object dataToExport, string? outputPath);
}
