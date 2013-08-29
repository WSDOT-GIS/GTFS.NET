using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtfs.Contract
{
	public class Route
	{
		[Required]
		public string route_id { get; set; }

		public string agency_id { get; set; }

		[Required]
		public string route_short_name { get; set; }

		[Required]
		public string route_long_name { get; set; }

		public string route_desc { get; set; }

		[Required]
		public RouteType route_type { get; set; }

		public Uri route_url { get; set; }

		public int? route_color { get; set; }

		public int? route_text_color { get; set; }
	}
}
