using CsvHelper.Configuration;
using Wsdot.Gtfs.Contract;

namespace Wsdot.Gtfs.IO.ClassMap
{
	public class CalendarDateMap: CsvClassMap<CalendarDate>
	{
		public override void CreateMap()
		{
			Map(m => m.service_id);
			Map(m => m.date).ConvertUsing(row => row.GetField<string>("date").ParseGtfsDate());
			Map(m => m.exception_type);
		}
	}
}
