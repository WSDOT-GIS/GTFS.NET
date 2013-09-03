using CsvHelper.Configuration;
using Wsdot.Gtfs.Contract;

namespace Wsdot.Gtfs.IO.ClassMap
{
	public class CalendarMap: CsvClassMap<Calendar>
	{
		public override void CreateMap()
		{
			Map(m => m.service_id);
			Map(m => m.monday);
			Map(m => m.tuesday);
			Map(m => m.wednesday);
			Map(m => m.thursday);
			Map(m => m.friday);
			Map(m => m.saturday);
			Map(m => m.sunday);
			Map(m => m.start_date).ConvertUsing(row =>  row.GetField<string>("start_date").ParseGtfsDate());
			Map(m => m.end_date).ConvertUsing(row => row.GetField<string>("end_date").ParseGtfsDate());
		}
	}
}
