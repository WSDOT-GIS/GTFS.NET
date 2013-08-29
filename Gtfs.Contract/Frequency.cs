using System;

namespace Gtfs.Contract
{
	public class Frequency
	{
		[Required]
		public string trip_id { get; set; }

		[Required]
		public TimeSpan start_time { get; set; }

		[Required]
		public TimeSpan end_time { get; set; }

		[Required]
		public int headway_secs { get; set; }

		public bool? exact_times { get; set; }
	}
}
