using GMYEL8_HSZF_2024251.Model.Entities;

namespace GMYEL8_HSZF_2024251.Model.Statistics;

public class TaxiCarServiceStatistic
{
	public TaxiCar TaxiCar { get; set; } = default!;
	public double AverageDistance { get; set; }
	public Service LongestTrip { get; set; } = default!;
	public Service ShortestTrip { get; set; } = default!;
}
