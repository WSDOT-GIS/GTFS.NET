using System;

namespace Gtfs.Contract
{
	public class Calendar
	{
		[Required]
		public string service_id { get; set; }

		[Required]
		public bool monday { get; set; }
		[Required]
		public bool tuesday { get; set; }
		[Required]
		public bool wednesday { get; set; }
		[Required]
		public bool thursday { get; set; }
		[Required]
		public bool friday { get; set; }
		[Required]
		public bool saturday { get; set; }
		[Required]
		public bool sunday { get; set; }

		[Required]
		public DateTime start_date { get; set; }
		[Required]
		public DateTime end_date { get; set; }
	}
}
