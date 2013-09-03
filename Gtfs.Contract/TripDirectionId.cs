namespace Gtfs.Contract
{
	/// <summary>
	/// A binary value that indicates the direction of travel for a trip. 
	/// Use this field to distinguish between bi-directional trips with the same route_id. 
	/// This field is not used in routing; it provides a way to separate trips by direction
	/// when publishing time tables. You can specify names for each direction 
	/// with the trip_headsign field.
	/// </summary>
	public enum TripDirectionId
	{
		/// <summary>
		/// Travel in one direction (e.g. outbound travel) 
		/// </summary>
		MainDirection = 0,
		/// <summary>
		/// Travel in the opposite direction (e.g. inbound travel)
		/// </summary>
		OppositeDirection = 1
	}
}
