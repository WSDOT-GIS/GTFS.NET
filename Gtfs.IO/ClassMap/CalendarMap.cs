using CsvHelper.Configuration;
using Gtfs.Contract;

namespace Gtfs.IO.ClassMap
{
	public class CalendarMap: CsvClassMap<Calendar>
	{
		public override void CreateMap()
		{
			Map(m => m.service_id).Name("service_id");
			Map(m => m.monday).Name("monday");
			Map(m => m.tuesday).Name("tuesday");
			Map(m => m.wednesday).Name("wednesday");
			Map(m => m.thursday).Name("thursday");
			Map(m => m.friday).Name("friday");
			Map(m => m.saturday).Name("saturday");
			Map(m => m.sunday).Name("sunday");
			Map(m => m.start_date).ConvertUsing(row =>  row.GetField<string>("start_date").ParseGtfsDate());
			Map(m => m.end_date).ConvertUsing(row => row.GetField<string>("end_date").ParseGtfsDate());
		}
	}
}
