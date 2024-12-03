using ConsoleTools;

using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Application.Definitions.TaxiCarServices;
using GMYEL8_HSZF_2024251.Model.Entities;

using Con = System.Console;

namespace GMYEL8_HSZF_2024251.Console.UserInteractions;

public class DataMaintainInteraction(ITaxiCarCRUDService taxiCarCRUDService, ITaxiRouteService taxiRouteService) : IUserInteraction
{
    private readonly ITaxiCarCRUDService _taxiCarCRUDService = taxiCarCRUDService;
    private readonly ITaxiRouteService _taxiRouteService = taxiRouteService;

    public async Task ExecuteAsync()
    {
        var subMenu = new ConsoleMenu()
            .Add("Add taxi car", async () => await AddTaxiCarAsync())
            .Add("Delete taxi car", async () => await DeleteTaxiCarAsync())
            .Add("Get taxi car by licencse plate", async () => await GetTaxiCarByLicensePlateAsync())
            .Add("Update taxi car", async () => await UpdateTaxiCarAsync())
            .Add("Add taxi route", async () => await AddTaxiRouteAsync())
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

        try
        {
            await _taxiCarCRUDService.CreateTaxiCarAsync(taxiCar);
            Con.WriteLine("Taxi car added successfully.");
        }
        catch (Exception ex)
        {
            Con.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task DeleteTaxiCarAsync()
    {
        Con.WriteLine("Delete taxi car:");
        var licensePlate = GetUserInput("License plate: ");

        try
        {
            var taxiCar = await _taxiCarCRUDService.GetTaxiCarByIdAsync(licensePlate);

            await _taxiCarCRUDService.DeleteTaxiCarAsync(taxiCar);
            Con.WriteLine("Taxi car deleted successfully.");
        }
        catch (Exception ex)
        {
            Con.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task GetTaxiCarByLicensePlateAsync()
    {
        Con.WriteLine("Get taxi car by licencse plate:");
        var licensePlate = GetUserInput("License plate: ");

        try
        {
            var taxiCar = await _taxiCarCRUDService.GetTaxiCarByIdAsync(licensePlate);

            Con.WriteLine(taxiCar);
        }
        catch (Exception ex)
        {
            Con.WriteLine($"Error: {ex.Message}");
        }
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

        try
        {
            await _taxiCarCRUDService.UpdateTaxiCarAsync(updatedTaxiCar);

            Con.WriteLine("Taxi car updated successfully.");
        }
        catch (Exception ex)
        {
            Con.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task AddTaxiRouteAsync()
    {
        Con.WriteLine("Add new taxi route:");
        var licensePlate = GetUserInput("License plate: ");
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

        try
        {
            _taxiRouteService.FareExceeded += (sender, args) =>
            {
                Con.WriteLine($"Warning: The fare ({args.PaidAmount} HUF) exceeds the allowed threshold ({args.Threshold} HUF)!");
            };

            await _taxiRouteService.AddTaxiRouteAsync(taxiService, licensePlate);
            Con.WriteLine("Taxi route added successfully.");
        }
        catch (Exception ex)
        {
            Con.WriteLine($"Error: {ex.Message}");
        }
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
}
