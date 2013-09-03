
namespace Wsdot.Gtfs.Contract
{
	/// <summary>
	/// Individual location where vehicles pick up or drop off passengers.
	/// </summary>
	public class Stop
	{
		/// <summary>
		/// The stop_id field contains an ID that uniquely identifies a stop or station. 
		/// Multiple routes may use the same stop. 
		/// The stop_id is dataset unique.
		/// </summary>
		[Required, Unique]
		public string stop_id { get; set; }
		/// <summary>
		/// <para>The stop_code field contains short text or a number that uniquely identifies the stop for passengers. 
		/// Stop codes are often used in phone-based transit information systems or printed on stop signage to make it 
		/// easier for riders to get a stop schedule or real-time arrival information for a particular stop.</para>
		/// <para>The stop_code field should only be used for stop codes that are displayed to passengers. 
		/// For internal codes, use stop_id. This field should be left blank for stops without a code.</para>
		/// </summary>
		public string stop_code { get; set; }
		/// <summary>
		/// The stop_name field contains the name of a stop or station. 
		/// Please use a name that people will understand in the local and tourist vernacular.
		/// </summary>
		[Required]
		public string stop_name { get; set; }
		/// <summary>
		/// The stop_desc field contains a description of a stop. Please provide useful, quality information. 
		/// Do not simply duplicate the name of the stop.
		/// </summary>
		public string stop_desc { get; set; }
		/// <summary>
		/// The stop_lat field contains the latitude of a stop or station. The field value must be a valid WGS 84 latitude.
		/// </summary>
		[Required]
		public double stop_lat { get; set; }
		/// <summary>
		/// The stop_lon field contains the longitude of a stop or station. The field value must be a valid WGS 84 longitude
		/// value from -180 to 180.
		/// </summary>
		[Required]
		public double stop_lon { get; set; }
		/// <summary>
		/// The zone_id field defines the fare zone for a stop ID. Zone IDs are required if you want to provide fare 
		/// information using fare_rules.txt. If this stop ID represents a station, the zone ID is ignored.
		/// </summary>
		public string zone_id { get; set; }
		/// <summary>
		/// <para>The stop_url field contains the URL of a web page about a particular stop. 
		/// This should be different from the agency_url and the route_url fields.</para>
		/// <para>The value must be a fully qualified URL that includes http:// or https://, 
		/// and any special characters in the URL must be correctly escaped. 
		/// See <see href="http://www.w3.org/Addressing/URL/4_URI_Recommentations.html" />
		/// for a description of how to create fully qualified URL values.</para>
		/// </summary>
		public string stop_url { get; set; }
		/// <summary>
		/// <para>The location_type field identifies whether this stop ID represents a stop or station.
		/// If no location type is specified, or the location_type is blank, stop IDs are treated as stops. 
		/// Stations may have different properties from stops when they are represented on a map or used in trip planning.</para>
		/// <para>The location type field can have the following values:</para>
		/// <para>0 or blank - Stop. A location where passengers board or disembark from a transit vehicle.</para>
		/// <para>1 - Station. A physical structure or area that contains one or more stop.</para>
		/// </summary>
		public StopLocationType? location_type { get; set; }
		/// <summary>
		/// For stops that are physically located inside stations, the parent_station field identifies the station associated with the stop. To use this field, stops.txt must also contain a row where this stop ID is assigned location type=1.
		/// <table border="1" cellpadding="1" cellspacing="1">
		/// <tbody><tr align="left">
		///   <th scope="col">This stop ID represents... </th>
		///   <th scope="col">This entry's location type...</th>
		///   <th scope="col">This entry's parent_station field contains...</th>
		/// </tr>
		/// <tr>
		///   <td scope="row">A stop located inside a  station.</td>
		///   <td>0 or blank </td>
		///   <td>The stop ID of the station where this stop is located. The stop
		///     referenced by parent_station must have location_type=1.</td>
		/// </tr>
		/// <tr>
		///   <td scope="row">A stop located outside a  station.</td>
		///   <td>0 or blank </td>
		///   <td>A blank value. The parent_station field doesn't apply to this
		///     stop. </td>
		/// </tr>
		/// <tr>
		///   <td scope="row">A station.</td>
		///   <td>1</td>
		///   <td>A blank value. Stations can't contain other stations.</td>
		/// </tr>
		/// </tbody></table>
		/// </summary>
		public string parent_station { get; set; }
		/// <summary>
		/// <para>The stop_timezone field contains the timezone in which this stop or station is located. 
		/// Please refer to <see href="http://en.wikipedia.org/wiki/List_of_tz_zones">Wikipedia List of Timezones</see> 
		/// for a list of valid values. 
		/// If omitted, the stop should be assumed to be located in the timezone specified by <see cref="Agendy.agency_timezone"/>.
		/// </para>
		/// <para>When a stop has a parent station, the stop is considered to be in the timezone specified by the parent station's 
		/// stop_timezone value. If the parent has no stop_timezone value, the stops that belong to that station are assumed to 
		/// be in the timezone specified by agency_timezone, even if the stops have their own stop_timezone values.
		/// In other words, if a given stop has a parent_station value, 
		/// any stop_timezone value specified for that stop must be ignored.</para>
		/// <para>Even if stop_timezone values are provided in stops.txt, the times in stop_times.txt 
		/// should continue to be specified as time since midnight in the timezone specified by agency_timezone in agency.txt. 
		/// This ensures that the time values in a trip always increase over the course of a trip, regardless of which timezones 
		/// the trip crosses.</para>
		/// </summary>
		public string stop_timezone { get; set; }
		
		/// <para>The <strong>wheelchair_boarding</strong> field identifies whether
		///   wheelchair boardings are possible from the specified stop or station.
		///   The field can have the following values:</para>
		/// <ul>
		///   <li><strong>0</strong> (or empty) - indicates that there is no
		///     accessibility information for the stop</li>
		///   <li><strong>1</strong> - indicates that at least some vehicles at
		///     this stop can be boarded by a rider in a wheelchair</li>
		///   <li><strong>2</strong> - wheelchair boarding is not possible at this
		///     stop</li>
		/// </ul>
		/// <para>When a stop is part of a larger station complex, as indicated by a
		///   stop with a <strong>parent_station</strong> value, the stop's
		///   <strong>wheelchair_boarding</strong> field has the following
		///   additional semantics:</para>
		/// <ul>
		///   <li><strong>0</strong> (or empty) - the stop will inherit its
		///     <strong>wheelchair_boarding</strong> value from the parent station,
		///     if specified in the parent</li>
		///   <li><strong>1</strong> - there exists some accessible path from
		///     outside the station to the specific stop / platform</li>
		///   <li><strong>2</strong> - there exists no accessible path from outside
		///     the station to the specific stop / platform</li>
		/// </ul>
		public WheelchairAccessibility? wheelchair_boarding { get; set; }

	}
}
