using CsvHelper.Configuration;
using Wsdot.Gtfs.Contract;

namespace Wsdot.Gtfs.IO.ClassMap
{
	public class StopTimeMap: CsvClassMap<StopTime>
	{
		public override void CreateMap()
		{
			Map(m => m.trip_id);
			Map(m => m.arrival_time).ConvertUsing(row => row.GetField<string>("arrival_time").ParseGtfsNullableTimeSpan());
			Map(m => m.departure_time).ConvertUsing(row => row.GetField<string>("departure_time").ParseGtfsNullableTimeSpan()); ;
			Map(m => m.stop_id);
			Map(m => m.stop_sequence);
			Map(m => m.stop_headsign);
			Map(m => m.pickup_type);
			Map(m => m.drop_off_type);
			Map(m => m.shape_dist_travelled);
		}
	}
}
