using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Wsdot.Gtfs.Test
{
	public static class Extensions
	{

		/// <summary>
		/// Determines if a <paramref name="value"/> is within a certain range.
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <param name="min">The minimum valid value.</param>
		/// <param name="max">The maximum valid value</param>
		/// <returns>value &gt;= min &amp;&amp; value &lt;= max</returns>
		public static bool IsInRange(this int value, int min, int max)
		{
			return value >= min && value <= max;
		}

		/// <summary>
		/// Determines if a <paramref name="value"/> is within a certain range.
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <param name="min">The minimum valid value.</param>
		/// <param name="max">The maximum valid value</param>
		/// <returns>
		/// Returns <see langword="true"/> if <paramref name="value"/> is between <paramref name="min"/> 
		/// and <paramref name="max"/> (inclusive), <see langword="false"/> otherwise.
		/// </returns>
		public static bool IsInRange(this double value, double min, double max)
		{
			return value >= min && value <= max;
		}

		/// <summary>
		/// Determines if a latitude value is valid.
		/// </summary>
		/// <param name="latitude">A latitude value.</param>
		/// <returns>
		/// Returns <see langword="true"/> if <paramref name="latitude"/> is between -90 and 90 (inclusive), 
		/// <see langword="false"/> otherwise.
		/// </returns>
		public static bool IsValidLatitude(this double latitude)
		{
			return latitude.IsInRange(-90, 90);
		}

		/// <summary>
		/// Determines if a longitude value is valid.
		/// </summary>
		/// <param name="longitude">A longitude value.</param>
		/// <returns>
		/// Returns <see langword="true"/> if <paramref name="longitude"/> is between -180 and 180 (inclusive), 
		/// <see langword="false"/> otherwise.
		/// </returns>
		public static bool IsValidLongitude(this double longitude)
		{
			return longitude.IsInRange(-180, 180);
		}

		public static bool HasValidLongitudeAndLatitude(this IEnumerable<double?> coordinates)
		{
			if (coordinates.Count() < 2)
			{
				return false;
			}
			return coordinates.ElementAt(0).Value.IsValidLongitude() && coordinates.ElementAt(1).Value.IsValidLongitude();
		}

		public static bool HasValidLongitudesAndLatitudes<T>(this IEnumerable<T> coordinates) where T : IEnumerable<double?>
		{
			bool output = true;
			foreach (var item in coordinates)
			{
				if (!item.HasValidLongitudeAndLatitude())
				{
					output = false;
					break;
				}
			}
			return output;
		}

		/// <summary>
		/// Gets a test property from a test method.
		/// </summary>
		/// <param name="memberInfo">A test method</param>
		/// <param name="name">The name of the property you want to get the value for.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">Thrown if either parameter is <see langword="null"/>.</exception>
		/// <example>
		/// <code><![CDATA[
		/// [TestMethod]
		/// [TestProperty("url", "http://www.gtfs-data-exchange.com/agency/jefferson-transit-authority/latest.zip")]
		/// public void ReadGtfsFromWeb()
		/// {
		/// 	var zipUrl = MethodInfo.GetCurrentMethod().GetTestProperty("url");
		/// 	
		/// 	// Do something...
		/// }
		/// ]]></code>
		/// </example>
		public static string GetTestProperty(this MemberInfo memberInfo, string name)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			else if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return memberInfo.GetCustomAttributes(typeof(TestPropertyAttribute), false)
				.Select(a => (TestPropertyAttribute)a)
				.First(a => a.Name == name)
				.Value;
		}
	}
}
