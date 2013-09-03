using System;

namespace Gtfs.Contract
{
	/// <summary>
	/// Headway (time between trips) for routes with variable frequency of service.
	/// </summary>
	public class Frequency
	{
		/// <summary>
		/// The trip_id contains an ID that identifies a trip on which the specified frequency of service applies. Trip IDs are referenced from the trips.txt file.
		/// </summary>
		[Required]
		public string trip_id { get; set; }

		/// <summary>
		/// The start_time field specifies the time at which service begins with the specified frequency. The time is measured from "noon minus 12h" (effectively midnight, except for days on which daylight savings time changes occur) at the beginning of the service date. For times occurring after midnight, enter the time as a value greater than 24:00:00 in HH:MM:SS local time for the day on which the trip schedule begins. 
		/// <example>25:35:00</example>.
		/// </summary>
		[Required]
		public TimeSpan start_time { get; set; }

		/// <summary>
		/// The end_time field indicates the time at which service changes to a different frequency (or ceases) at the first stop in the trip. The time is measured from "noon minus 12h" (effectively midnight, except for days on which daylight savings time changes occur) at the beginning of the service date. For times occurring after midnight, enter the time as a value greater than 24:00:00 in HH:MM:SS local time for the day on which the trip schedule begins. 
		/// <example>25:35:00</example>.
		/// </summary>
		[Required]
		public TimeSpan end_time { get; set; }

		/// <summary>
		/// The headway_secs field indicates the time between departures from the same stop (headway) for this trip type, during the time interval specified by start_time and end_time. The headway value must be entered in seconds.
		/// Periods in which headways are defined (the rows in frequencies.txt) shouldn't overlap for the same trip, since it's hard to determine what should be inferred from two overlapping headways. However, a headway period may begin at the exact same time that another one ends.
		/// </summary>
		[Required]
		public int headway_secs { get; set; }

		/// <summary>
		/// The exact_times field determines if frequency-based trips should be exactly scheduled based on the specified headway information. 
		/// </summary>
		public bool? exact_times { get; set; }
	}
}
