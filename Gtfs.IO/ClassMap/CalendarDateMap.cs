using CsvHelper.Configuration;
using Gtfs.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gtfs.IO.ClassMap
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
