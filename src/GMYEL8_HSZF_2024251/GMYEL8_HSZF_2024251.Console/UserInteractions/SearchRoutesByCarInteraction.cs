using GMYEL8_HSZF_2024251.Application.Definitions.SearchServices;
using GMYEL8_HSZF_2024251.Model.Search.Criterias;

using Con = System.Console;

namespace GMYEL8_HSZF_2024251.Console.UserInteractions;

/// <inheritdoc cref="IUserInteraction"/>
public class SearchRoutesByCarInteraction(ITaxiCarSearchService taxiCarSearchService) : IUserInteraction
{
	private readonly ITaxiCarSearchService _taxiCarSearchService = taxiCarSearchService;

	public async Task ExecuteAsync()
	{
		const string PrimarySeparator = "===============================";
		const string SecondarySeparator = "-------------------------------";

		Con.WriteLine("Search taxi routes by taxi cars");
		Con.WriteLine(PrimarySeparator);

		string? licensePlate = GetStringUserInput("Taxi car's license plate (optional): ");
		string? driverName = GetStringUserInput("Taxi car's driver (optional): ");

		Con.Write("Page number: ");
		if (!int.TryParse(Con.ReadLine(), out int pageNumber))
		{
			pageNumber = 1;
		}

		Con.Write("Page size: ");
		if (!int.TryParse(Con.ReadLine(), out int pageSize))
		{
			pageSize = 10;
		}

		var searchCriteria = new TaxiCarSearchCriteria
		{
			Driver = driverName,
			LicensePlate = licensePlate,
			PageNumber = pageNumber,
			PageSize = pageSize,
		};

		var searchResult = await _taxiCarSearchService.SearchTaxiCarsAsync(searchCriteria);

		Con.WriteLine(PrimarySeparator);

		Con.WriteLine("Search results:");

		if (searchResult.Items.Count > 0)
		{
			Con.WriteLine($"Total search results count: {searchResult.TotalCount}");
			Con.WriteLine($"Current page: {searchResult.CurrentPage}/{(int)Math.Ceiling((double)searchResult.TotalCount / searchResult.PageSize)}");
			Con.WriteLine($"Taxi car: {searchResult.Items[0].TaxiCar}");

			Con.WriteLine($"Found services for taxi car:");

			foreach (var route in searchResult.Items)
			{
				Con.WriteLine($"\t{SecondarySeparator}");
				Con.WriteLine($"\t{route}");
			}
		}
		else
		{
			Con.WriteLine($"Taxi car with the given context not found");
		}

		Con.WriteLine(PrimarySeparator);
		Con.WriteLine("Press any key to return to the menu.");
		Con.ReadKey();
	}

	private static string? GetStringUserInput(string prompt)
	{
		Con.Write(prompt);
		return Con.ReadLine();
	}
}
