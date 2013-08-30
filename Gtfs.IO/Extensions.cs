using Gtfs.IO.Properties;
using System;
using System.Globalization;

namespace Gtfs.IO
{
	public static class Extensions
	{
		public static DateTime ParseGtfsDate(this string s)
		{
			return DateTime.ParseExact(s, Resources.DateFormat, CultureInfo.InvariantCulture);
		}

		public static DateTime? ParseGtfsNullableDate(this string s)
		{
			return string.IsNullOrWhiteSpace(s) ? default(DateTime?) : s.ParseGtfsDate();
		}
	}
}
