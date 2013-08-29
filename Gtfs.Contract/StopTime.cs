using System;

namespace Gtfs.Contract
{
	public class StopTime
	{
		[Required]
		public string trip_id { get; set; }

		[Required]
		public TimeSpan arrival_time { get; set; }

		[Required]
		public TimeSpan departure_time { get; set; }

		[Required]
		public string stop_id { get; set; }

		[Required]
		public uint stop_sequence { get; set; }

		public string stop_headsign { get; set; }

		public PickupOrDropOffType? pickup_type { get; set; }

		public PickupOrDropOffType? drop_off_type { get; set; }

		public float? shape_dist_travelled { get; set; }
		
	}
}
