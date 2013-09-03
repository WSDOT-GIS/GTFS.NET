
namespace Wsdot.Gtfs.Contract
{
	/// <summary>
	/// Trips for each route. A trip is a sequence of two or more stops that occurs at specific time.
	/// </summary>
	public class Trip
	{
		/// <summary>
		/// The route_id field contains an ID that uniquely identifies a route. This value is referenced from the routes.txt file.
		/// </summary>
		/// <seealso cref="Route.route_id"/>
		[Required]
		public string route_id { get; set; }

		/// <summary>
		/// The service_id contains an ID that uniquely identifies a set of dates when service is available for one or more routes. 
		/// This value is referenced from the calendar.txt or calendar_dates.txt file.
		/// </summary>
		/// <seealso cref="Calendar"/>
		/// <seealso cref="CalendarDate"/>
		[Required]
		public string service_id { get; set; }

		/// <summary>
		/// The trip_id field contains an ID that identifies a trip. The trip_id is dataset unique.
		/// </summary>
		[Required,Unique]
		public string trip_id { get; set; }

		/// <summary>
		///  The trip_headsign field contains the text that appears on a sign that identifies the trip's destination to passengers.
		///  Use this field to distinguish between different patterns of service in the same route.
		///  If the headsign changes during a trip, you can override the trip_headsign by specifying values 
		///  for <see cref="StopTime.stop_headsign">the stop_headsign field in stop_times.txt</see>.
		/// </summary>
		public string trip_headsign { get; set; }

		/// <summary>
		/// <para>The trip_short_name field contains the text that appears in schedules and sign boards to identify the trip to passengers, for example, to identify train numbers for commuter rail trips. If riders do not commonly rely on trip names, please leave this field blank.</para>
		/// <para>A trip_short_name value, if provided, should uniquely identify a trip within a service day; it should not be used for destination names or limited/express designations.</para>
		/// </summary>
		public string trip_shortname { get; set; }

		/// <summary>
		/// <para>The direction_id field contains a binary value that indicates the direction of travel for a trip. 
		/// Use this field to distinguish between bi-directional trips with the same route_id. 
		/// This field is not used in routing; it provides a way to separate trips by direction when publishing time tables. 
		/// You can specify names for each direction with <see cref="Trip.trip_headsign">the trip_headsign field</see>.
		/// </para>
		/// <para>For example, you could use the trip_headsign and direction_id fields together to
		/// assign a name to travel in each direction for a set of trips.</para>
		/// </summary>
		public TripDirectionId? direction_id { get; set; }

		/// <summary>
		/// The block_id field identifies the block to which the trip belongs. 
		/// A block consists of two or more sequential trips made using the same vehicle,
		/// where a passenger can transfer from one trip to the next just by staying in the vehicle. 
		/// The block_id must be referenced by two or more trips in trips.txt.
		/// </summary>
		public string block_id { get; set; }

		/// <summary>
		/// The shape_id field contains an ID that defines a shape for the trip. This value is referenced from the shapes.txt file. 
		/// The shapes.txt file allows you to define how a line should be drawn on the map to represent a trip.
		/// </summary>
		public string shape_id { get; set; }

		/// <summary>
		/// Indicates wheelchair accessibility
		/// </summary>
		public WheelchairAccessibility wheelchair_accessible { get; set; }
	}
}
