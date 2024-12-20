using GMYEL8_HSZF_2024251.Application.Definitions;
using GMYEL8_HSZF_2024251.Application.Events;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Persistence.MsSql.DataProviders.Definitions;

namespace GMYEL8_HSZF_2024251.Application.Implementations;

/// <inheritdoc cref="ITaxiRouteService"/>
public class TaxiRouteService(ITaxiRouteServiceDataProvider taxiRouteServiceDataProvider) : ITaxiRouteService
{
	private readonly ITaxiRouteServiceDataProvider _taxiRouteServiceDataProvider = taxiRouteServiceDataProvider;

	public event EventHandler<FareExceededEventArgs>? FareExceeded;

	public async Task AddTaxiRouteAsync(Service taxiService, string taxiCarId)
	{
		int maxServicePrice = await _taxiRouteServiceDataProvider.GetMaxServicePriceByCarAsync(taxiService.TaxiCarId);

		int threshold = maxServicePrice * 2;

		if (maxServicePrice != 0 && taxiService.PaidAmount > threshold)
		{
			OnFareExceeded(new FareExceededEventArgs(taxiService.PaidAmount, threshold));
		}

		taxiService.TaxiCarId = taxiCarId;

		await _taxiRouteServiceDataProvider.AddTaxiRouteAsync(taxiService);
	}

	public void OnFareExceeded(FareExceededEventArgs e)
	{
		FareExceeded?.Invoke(this, e);
	}
}
