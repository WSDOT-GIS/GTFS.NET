using System.Collections.Generic;

namespace Wsdot.Gtfs.Contract
{
	public class GtfsFeed
	{
		/// <summary>
		/// One or more transit agencies that provide the data in this feed.
		/// </summary>
		[Required]
		public List<Agency> Agency { get; set; }

		/// <summary>
		/// Individual locations where vehicles pick up or drop off passengers.
		/// </summary>
		[Required]
		public List<Stop> Stops { get; set; }

		/// <summary>
		/// Transit routes. A route is a group of trips that are displayed to riders as a single service.
		/// </summary>
		[Required]
		public List<Route> Routes { get; set; }

		/// <summary>
		/// Trips for each route. A trip is a sequence of two or more stops that occurs at specific time.
		/// </summary>
		[Required]
		public List<Trip> Trips { get; set; }

		/// <summary>
		/// Times that a vehicle arrives at and departs from individual stops for each trip.
		/// </summary>
		[Required]
		public List<StopTime> StopTimes { get; set; }

		/// <summary>
		/// Dates for service IDs using a weekly schedule. Specify when service starts and ends, as well as days of 
		/// the week where service is available.
		/// </summary>
		[Required]
		public List<Calendar> Calendar { get; set; }

		/// <summary>
		/// Exceptions for the service IDs defined in the calendar.txt file. 
		/// If calendar_dates.txt includes ALL dates of service, this file may be specified instead of calendar.txt.
		/// </summary>
		public List<CalendarDate> CalendarDates { get; set; }

		/// <summary>
		/// Fare information for a transit organization's routes.
		/// </summary>
		public List<FareAttribute> FareAttributes { get; set; }

		/// <summary>
		/// Rules for applying fare information for a transit organization's routes.
		/// </summary>
		public List<FareRule> FareRules { get; set; }

		/// <summary>
		/// Rules for drawing lines on a map to represent a transit organization's routes.
		/// </summary>
		public List<Shape> Shapes { get; set; }

		/// <summary>
		/// Headway (time between trips) for routes with variable frequency of service.
		/// </summary>
		public List<Frequency> Frequencies { get; set; }

		/// <summary>
		/// Rules for making connections at transfer points between routes. 
		/// </summary>
		public List<Transfer> Transfers { get; set; }

		/// <summary>
		/// Additional information about the feed itself, including publisher, version, and expiration information.
		/// </summary>
		public FeedInfo FeedInfo { get; set; }
	}
}
