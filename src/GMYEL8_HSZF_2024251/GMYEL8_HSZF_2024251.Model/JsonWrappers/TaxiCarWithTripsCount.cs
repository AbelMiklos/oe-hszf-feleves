using GMYEL8_HSZF_2024251.Model.Entities;

namespace GMYEL8_HSZF_2024251.Model.JsonWrappers;

public class TaxiCarWithTripsCount
{
	public TaxiCar TaxiCar { get; set; } = default!;
	public int TripsCount { get; set; }
}
