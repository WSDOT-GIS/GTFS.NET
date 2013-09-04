using System;
using System.Globalization;
using Wsdot.Gtfs.IO.Properties;

namespace Wsdot.Gtfs.IO
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

		public static TimeSpan? ParseGtfsNullableTimeSpan(this string s)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return null;
			}
			TimeSpan ts;
			if (TimeSpan.TryParse(s, out ts))
			{
				return ts;
			}
			else
			{
				return null;
			}
		}
	}
}
