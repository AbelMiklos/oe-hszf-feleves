using GMYEL8_HSZF_2024251.Application.Events;
using GMYEL8_HSZF_2024251.Model.Entities;
using GMYEL8_HSZF_2024251.Model.Exceptions;

namespace GMYEL8_HSZF_2024251.Application.Definitions;

/// <summary>
///     Handles operations related to taxi routes.
/// </summary>
public interface ITaxiRouteService
{
	/// <summary>
	///     Triggered when the fare exceeds a certain limit.
	/// </summary>
	event EventHandler<FareExceededEventArgs> FareExceeded;

	/// <summary>
	///     Adds a new taxi route.
	/// </summary>
	/// <param name="taxiService">The service details of the taxi route to be added.</param>
	/// <exception cref="BusinessException">Thrown when the provided taxi car license plate does not match any of the existing taxi car licenses.</exception>
	Task AddTaxiRouteAsync(Service taxiService, string taxiCarId);
}
