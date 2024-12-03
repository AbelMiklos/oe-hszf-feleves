namespace GMYEL8_HSZF_2024251.Application.Definitions;

public interface IFileExportService
{
    /// <summary>
    ///     
    /// </summary>
    /// <param name="dataToExport"></param>
    /// <param name="outputPath"></param>
    //void ExportData(TaxiCarWrapper dataToExport, string? outputPath);
    void ExportData(object dataToExport, string? outputPath);
}
