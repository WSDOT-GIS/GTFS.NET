using CsvHelper.Configuration;
using Wsdot.Gtfs.Contract;

namespace Wsdot.Gtfs.IO.ClassMap
{
	/// <summary>
	/// Provides CSV &lt;-&gt; class mapping for <see cref="CalendarDate"/>.
	/// </summary>
	public class CalendarDateMap: CsvClassMap<CalendarDate>
	{
		/// <summary>
		/// Creates CSV mapping.
		/// </summary>
		public override void CreateMap()
		{
			Map(m => m.service_id);
			Map(m => m.date).ConvertUsing(row => row.GetField<string>("date").ParseGtfsDate());
			Map(m => m.exception_type);
		}
	}
}
