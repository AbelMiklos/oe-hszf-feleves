namespace GMYEL8_HSZF_2024251.Model.Search.Criterias;

public class TaxiCarSearchCriteria
{
	public string? LicensePlate { get; set; }
	public string? Driver { get; set; }
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
}
