
namespace Wsdot.Gtfs.Contract
{
	/// <summary>
	/// Rules for making connections at transfer points between routes. 
	/// </summary>
	public class Transfer
	{
		/// <summary>
		/// The from_stop_id field contains a stop ID that identifies a stop or station where a connection between routes begins. Stop IDs are referenced from the stops.txt file. If the stop ID refers to a station that contains multiple stops, this transfer rule applies to all stops in that station.
		/// </summary>
		[Required]
		public string from_stop_id { get; set; }
		/// <summary>
		/// The to_stop_id field contains a stop ID that identifies a stop or station where a connection between routes ends. Stop IDs are referenced from the stops.txt file. If the stop ID refers to a station that contains multiple stops, this transfer rule applies to all stops in that station.
		/// </summary>
		[Required]
		public string to_stop_id { get; set; }
		/// <summary>
		/// The transfer_type field specifies the type of connection for the specified (from_stop_id, to_stop_id) pair.
		/// </summary>
		[Required]
		public TransferType? transfer_type { get; set; }

		/// <summary>
		/// <para>When a connection between routes requires an amount of time between arrival and departure (transfer_type=2), the min_transfer_time field defines the amount of time that must be available in an itinerary to permit a transfer between routes at these stops. The min_transfer_time must be sufficient to permit a typical rider to move between the two stops, including buffer time to allow for schedule variance on each route.</para>
		/// <para>The min_transfer_time value must be entered in seconds, and must be a non-negative integer.</para>
		/// </summary>
		public int? min_transfer_time { get; set; }
	}
}
