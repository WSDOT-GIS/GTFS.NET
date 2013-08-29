using System;

namespace Gtfs.Contract
{
	public class FeedInfo
	{
		[Required]
		public string feed_publisher_name { get; set; }

		[Required]
		public string feed_publisher_url { get; set; }

		[Required]
		public string feed_lang { get; set; }

		public DateTime? feed_start_date { get; set; }

		public DateTime? feed_end_date { get; set; }

		public string feed_version { get; set; }
	}
}
