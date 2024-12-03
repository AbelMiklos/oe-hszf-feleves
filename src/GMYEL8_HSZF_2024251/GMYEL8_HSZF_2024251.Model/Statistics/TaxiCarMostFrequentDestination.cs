using GMYEL8_HSZF_2024251.Model.Entities;

namespace GMYEL8_HSZF_2024251.Model.Statistics;

public class TaxiCarMostFrequentDestination
{
    public TaxiCar TaxiCar { get; set; } = default!;
    public string MostFrequentDestination { get; set; } = string.Empty;
}
