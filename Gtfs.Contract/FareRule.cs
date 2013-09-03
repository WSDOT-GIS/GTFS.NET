
namespace Wsdot.Gtfs.Contract
{
	/// <summary>
	/// <para>The fare_rules table allows you to specify how fares in fare_attributes.txt apply to an itinerary. Most fare structures use some combination of the following rules:</para>
	/// <ul>
	///		<li>Fare depends on origin or destination stations.  </li>
	///		<li>Fare depends on which zones the itinerary passes through.  </li>
	///		<li>Fare depends on which route the itinerary uses.  </li>
	/// </ul>
	/// <para>For examples that demonstrate how to specify a fare structure with fare_rules.txt and fare_attributes.txt,
	/// see <see href="http://code.google.com/p/googletransitdatafeed/wiki/FareExamples">FareExamples</see> 
	/// in the GoogleTransitDataFeed open source project wiki.</para>
	/// </summary>
	public class FareRule
	{
		/// <summary>
		/// The fare_id field contains an ID that uniquely identifies a fare class. This value is referenced from the fare_attributes.txt file.
		/// </summary>
		[Required]
		public string fare_id { get; set; }
		/// <summary>
		/// The route_id field associates the fare ID with a route. Route IDs are referenced from the routes.txt file. If you have several routes with the same fare attributes, create a row in fare_rules.txt for each route.
		/// </summary>
		public string route_id { get; set; }
		/// <summary>
		/// The origin_id field associates the fare ID with an origin zone ID. Zone IDs are referenced from the stops.txt file. If you have several origin IDs with the same fare attributes, create a row in fare_rules.txt for each origin ID.
		/// </summary>
		public string origin_id { get; set; }
		/// <summary>
		/// The destination_id field associates the fare ID with a destination zone ID. Zone IDs are referenced from the stops.txt file. If you have several destination IDs with the same fare attributes, create a row in fare_rules.txt for each destination ID.
		/// </summary>
		public string destination_id { get; set; }
		/// <summary>
		/// The contains_id field associates the fare ID with a zone ID, referenced from the stops.txt file. The fare ID is then associated with itineraries that pass through every contains_id zone.
		/// </summary>
		public string contains_id { get; set; }
	}
}
