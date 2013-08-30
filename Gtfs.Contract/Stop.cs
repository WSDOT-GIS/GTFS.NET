
namespace Gtfs.Contract
{
	public class Stop
	{
		[Required]
		public string stop_id { get; set; }
		public string stop_code { get; set; }
		[Required]
		public string stop_name { get; set; }
		public string stop_desc { get; set; }
		[Required]
		public double stop_lat { get; set; }
		[Required]
		public double stop_lon { get; set; }
		public string zone_id { get; set; }
		public string stop_url { get; set; }
		public StopLocationType? location_type { get; set; }
		public string parent_station { get; set; }
		public string stop_timezone { get; set; }
		public WheelchairAccessibility? wheelchair_boarding { get; set; }

	}
}
