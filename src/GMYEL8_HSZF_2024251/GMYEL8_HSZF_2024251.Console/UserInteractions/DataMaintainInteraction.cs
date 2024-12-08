using ConsoleTools;

using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;
using GMYEL8_HSZF_2024251.Console.Middleware;
using GMYEL8_HSZF_2024251.Model.Entities;

using Con = System.Console;

namespace GMYEL8_HSZF_2024251.Console.UserInteractions;

public class DataMaintainInteraction(
	ITaxiCarCRUDService taxiCarCRUDService,
	ITaxiRouteService taxiRouteService,
	IMiddlewarePipeline middlewarePipeline) : IUserInteraction
{
	private readonly ITaxiCarCRUDService _taxiCarCRUDService = taxiCarCRUDService;
	private readonly ITaxiRouteService _taxiRouteService = taxiRouteService;

	private readonly IMiddlewarePipeline _middlewarePipeline = middlewarePipeline;

	public async Task ExecuteAsync()
	{
		var subMenu = new ConsoleMenuWithMiddleware(_middlewarePipeline)
			.Add("Add taxi car", AddTaxiCarAsync)
			.Add("Delete taxi car", DeleteTaxiCarAsync)
			.Add("Get taxi car by licencse plate", GetTaxiCarByLicensePlateAsync)
			.Add("Update taxi car", UpdateTaxiCarAsync)
			.Add("Add taxi route", AddTaxiRouteAsync)
			.Add("Back", ConsoleMenu.Close);

		await subMenu.ShowAsync();
	}

	private async Task AddTaxiCarAsync()
	{
		Con.WriteLine("Add new taxi car:");

		string licensePlate = GetUserInput("License plate: ");
		string driverName = GetUserInput("Driver name: ");

		var taxiCar = new TaxiCar
		{
			LicensePlate = licensePlate,
			Driver = driverName
		};

		await _taxiCarCRUDService.CreateTaxiCarAsync(taxiCar);

		DoneSuccessfully("Taxi car added successfully.");
	}

	private async Task DeleteTaxiCarAsync()
	{
		Con.WriteLine("Delete taxi car:");
		var licensePlate = GetUserInput("License plate: ");

		var taxiCar = await _taxiCarCRUDService.GetTaxiCarByIdAsync(licensePlate);

		await _taxiCarCRUDService.DeleteTaxiCarAsync(taxiCar);

		DoneSuccessfully("Taxi car deleted successfully.");
	}

	private async Task GetTaxiCarByLicensePlateAsync()
	{
		Con.WriteLine("Get taxi car by licencse plate:");
		var licensePlate = GetUserInput("License plate: ");

		var taxiCar = await _taxiCarCRUDService.GetTaxiCarByIdAsync(licensePlate);

		DoneSuccessfully(taxiCar.ToString());
	}

	private async Task UpdateTaxiCarAsync()
	{
		Con.WriteLine("Update taxi car:");
		var licensePlate = GetUserInput("License plate: ");
		var driverName = GetUserInput("Driver name: ");

		var updatedTaxiCar = new TaxiCar
		{
			LicensePlate = licensePlate,
			Driver = driverName
		};

		await _taxiCarCRUDService.UpdateTaxiCarAsync(updatedTaxiCar);

		DoneSuccessfully("Taxi car updated successfully.");
	}

	private async Task AddTaxiRouteAsync()
	{
		Con.WriteLine("Add new taxi route:");

		var licensePlate = GetUserInput("License plate: ");
		var taxiCar = await _taxiCarCRUDService.GetTaxiCarByIdAsync(licensePlate);
		var from = GetUserInput("Start location: ");
		var to = GetUserInput("Destination: ");
		var distance = GetIntUserInput("Distance (km): ");
		var paidAmount = GetIntUserInput("Fare (HUF): ");

		var taxiService = new Service
		{
			TaxiCarId = licensePlate,
			From = from,
			To = to,
			Distance = distance,
			PaidAmount = paidAmount,
			FareStartDate = DateTime.Now
		};

		_taxiRouteService.FareExceeded += (sender, args) =>
		{
			Con.WriteLine($"Warning: The fare ({args.PaidAmount} HUF) exceeds the allowed threshold ({args.Threshold} HUF)!");
		};

		await _taxiRouteService.AddTaxiRouteAsync(taxiService, licensePlate);

		DoneSuccessfully("Taxi route added successfully.");
	}

	private static string GetUserInput(string prompt)
	{
		Con.Write(prompt);
		return Con.ReadLine() ?? string.Empty;
	}

	private static int GetIntUserInput(string prompt)
	{
		int input = 0;

		try
		{
			input = int.Parse(GetUserInput(prompt));
		}
		catch (Exception)
		{
			GetIntUserInput(prompt);
		}

		return input;
	}

	private void DoneSuccessfully(string consoleOutput)
	{
		Con.ForegroundColor = ConsoleColor.Green;
		Con.WriteLine(consoleOutput);
		Con.ResetColor();
		Con.WriteLine("Press any key to return to the menu...");
		Con.ReadKey();
	}
}
