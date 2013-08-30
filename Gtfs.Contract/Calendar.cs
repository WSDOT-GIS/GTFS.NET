
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

		// TODO: start_date and end_date should really be a different type, e.g. timespan.
		[Required]
		public string start_date { get; set; }
		[Required]
		public string end_date { get; set; }
	}
}
