using CsvHelper.Configuration;
using System.Globalization;
using Wsdot.Gtfs.Contract;

namespace Wsdot.Gtfs.IO.ClassMap
{
	/// <summary>
	/// Provides CSV mapping for <see cref="Route"/>.
	/// </summary>
	public class RouteMap : CsvClassMap<Route>
	{
		/// <summary>Create CSV map.</summary>
		public override void CreateMap()
		{
			Map(m => m.route_id);
			Map(m => m.agency_id);
			Map(m => m.route_short_name);
			Map(m => m.route_long_name);
			Map(m => m.route_desc);
			Map(m => m.route_type);
			Map(m => m.route_url);
			Map(m => m.route_color).TypeConverterOption(NumberStyles.HexNumber);
			Map(m => m.route_text_color).TypeConverterOption(NumberStyles.HexNumber);
		}
	}
}
