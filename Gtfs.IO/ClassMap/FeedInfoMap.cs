using CsvHelper.Configuration;
using Wsdot.Gtfs.Contract;

namespace Wsdot.Gtfs.IO.ClassMap
{
	public class FeedInfoMap: CsvClassMap<FeedInfo>
	{
		public override void CreateMap()
		{
			Map(m => m.feed_publisher_name);
			Map(m => m.feed_publisher_url);
			Map(m => m.feed_lang);
			Map(m => m.feed_start_date).ConvertUsing(row => row.GetField<string>("feed_start_date").ParseGtfsNullableDate());
			Map(m => m.feed_end_date).ConvertUsing(row => row.GetField<string>("feed_end_date").ParseGtfsNullableDate());
		}
	}
}
