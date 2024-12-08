namespace GMYEL8_HSZF_2024251.Application.Events;

public class FareExceededEventArgs : EventArgs
{
	public decimal PaidAmount { get; }
	public decimal Threshold { get; }

	public FareExceededEventArgs(decimal paidAmount, decimal threshold)
	{
		PaidAmount = paidAmount;
		Threshold = threshold;
	}
}
