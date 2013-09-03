
namespace Wsdot.Gtfs.Contract
{
	/// <summary>
	/// The transfer_type field specifies the type of connection for the specified (<see cref="Transfer.from_stop_id"/>, 
	/// <see cref="Transfer.to_stop_id"/>) pair.
	/// </summary>
	public enum TransferType
	{
		/// <summary>
		/// This is a recommended transfer point between two routes.
		/// </summary>
		RecommendedTransferPoint = 0,
		/// <summary>
		/// This is a timed transfer point between two routes. The departing vehicle is expected to wait for the arriving one, with sufficient time for a passenger to transfer between routes.
		/// </summary>
		TimedTransferPoint = 1,
		/// <summary>
		/// This transfer requires a minimum amount of time between arrival and departure to ensure a connection. The time required to transfer is specified by <see cref="Transfer.min_transfer_time"/>.
		/// </summary>
		MinimumRequired = 2,
		/// <summary>
		/// Transfers are not possible between routes at this location.
		/// </summary>
		BetweenRouteTransfersImpossible = 3
	}
}
