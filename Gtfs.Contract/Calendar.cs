
using System;
namespace Wsdot.Gtfs.Contract
{
	public class Calendar
	{
		/// <summary>
		/// The service_id contains an ID that uniquely identifies a set of dates when service is available for one or more routes. 
		/// Each service_id value can appear at most once in a calendar.txt file. 
		/// This value is dataset unique.
		/// It is referenced by the trips.txt file.
		/// </summary>
		[Required,Unique]
		public string service_id { get; set; }

		/// <summary>Indicates whether the service is valid for all Mondays</summary>
		[Required]
		public bool monday { get; set; }
		/// <summary>Indicates whether the service is valid for all Tuesdays</summary>
		[Required]
		public bool tuesday { get; set; }
		/// <summary>Indicates whether the service is valid for all Wednesdays</summary>
		[Required]
		public bool wednesday { get; set; }
		/// <summary>Indicates whether the service is valid for all Thursdays</summary>
		[Required]
		public bool thursday { get; set; }
		/// <summary>Indicates whether the service is valid for all Fridays</summary>
		[Required]
		public bool friday { get; set; }
		/// <summary>Indicates whether the service is valid for all Saturdays</summary>
		[Required]
		public bool saturday { get; set; }
		/// <summary>Indicates whether the service is valid for all Sundays</summary>
		[Required]
		public bool sunday { get; set; }
		/// <summary>The start date for the service.</summary>
		[Required]
		public DateTime start_date { get; set; }
		/// <summary>The end date for the service</summary>
		[Required]
		public DateTime end_date { get; set; }
	}
}
