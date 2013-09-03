
namespace Gtfs.Contract
{
	/// <summary>
	/// Indicates when a fare must be paid. 
	/// </summary>
	public enum PaymentMethod
	{
		/// <summary>
		/// Fare is paid on board. 
		/// </summary>
		FareIsPaidOnBoard = 0,
		/// <summary>
		/// Fare must be paid before boarding. 
		/// </summary>
		FareMustBePaidBeforeBoarding = 1
	}
}
