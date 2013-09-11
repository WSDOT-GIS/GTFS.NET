using System;
using System.Globalization;
using Wsdot.Gtfs.IO.Properties;

namespace Wsdot.Gtfs.IO
{
	/// <summary>
	/// Provides extension methods.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Parses a date string in the format used by GTFS.
		/// </summary>
		/// <param name="s">A date in yyyyMMdd format.</param>
		/// <returns>
		/// Returns the <see cref="DateTime"/> represented by <paramref name="s"/>. 
		/// </returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="s"/> is <see langword="null"/>.</exception>
		/// <exception cref="FormatException">Thrown if <paramref name="s"/> is not in yyyyMMdd format.</exception>
		public static DateTime ParseGtfsDate(this string s)
		{
			return DateTime.ParseExact(s, Resources.DateFormat, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Parses a date string in the format used by GTFS.
		/// </summary>
		/// <param name="s">A date in yyyyMMdd format.</param>
		/// <returns>
		/// Returns the <see cref="DateTime"/> represented by <paramref name="s"/>. 
		/// If <paramref name="s"/> is <see langword="null"/> or <see cref="string.Empty"/>, 
		/// <see langword="null"/> will be returned.
		/// </returns>
		public static DateTime? ParseGtfsNullableDate(this string s)
		{
			return string.IsNullOrWhiteSpace(s) ? default(DateTime?) : s.ParseGtfsDate();
		}

		/// <summary>
		/// Parses a <see cref="TimeSpan"/> string in the format used by GTFS.
		/// </summary>
		/// <param name="s">A string representation of a <see cref="TimeSpan"/>.</param>
		/// <returns>
		/// Returns the <see cref="TimeSpan"/> represented by <paramref name="s"/>. 
		/// If <paramref name="s"/> is <see langword="null"/> or <see cref="string.Empty"/>, 
		/// <see langword="null"/> will be returned.
		/// </returns>
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
