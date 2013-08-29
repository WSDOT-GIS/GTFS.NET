using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtfs.Contract
{
	public class Feed
	{
		[Required]
		public List<Agency> Agency { get; set; }

		[Required]
		public List<Stop> Stops { get; set; }

		[Required]
		public List<Route> Routes { get; set; }

		[Required]
		public List<Trip> Trips { get; set; }

		[Required]
		public List<StopTime> StopTimes { get; set; }

		[Required]
		public List<Calendar> Calendar { get; set; }

		public List<CalendarDate> CalendarDates { get; set; }

		public List<FareAttribute> FareAttributes { get; set; }

		public List<FareRule> FareRules { get; set; }

		public List<Shape> Shapes { get; set; }

		public List<Frequency> Frequencies { get; set; }

		public List<Transfer> Transfers { get; set; }

		public FeedInfo FeedInfo { get; set; }
	}
}
