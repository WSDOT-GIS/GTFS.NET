using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtfs.Contract
{
	public class Trip
	{
		[Required]
		public string route_id { get; set; }

		[Required]
		public string service_id { get; set; }

		[Required]
		public string trip_id { get; set; }

		public string trip_headsign { get; set; }

		public string trip_shortname { get; set; }

		public TripDirectionId? direction_id { get; set; }

		public string block_id { get; set; }

		public string shape_id { get; set; }

		public WheelchairAccessibility wheelchair_accessible { get; set; }
	}
}
