using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Wsdot.Gtfs.Contract;
using Wsdot.Gtfs.IO;

namespace Wsdot.Gtfs.Test
{
	[TestClass]
	public class UnitTest1
	{


		[TestMethod]
		[TestProperty("gtfsFile", "sample-feed.zip")]
		public void ReadSampleGtfs()
		{
			var zipPath = MethodInfo.GetCurrentMethod().GetCustomAttributes(typeof(TestPropertyAttribute), false).Select(a => (TestPropertyAttribute)a).Where(a => a.Name == "gtfsFile").First().Value;

			Assert.IsTrue(File.Exists(zipPath), "File not found: {0}", Path.GetFullPath(zipPath));

			GtfsFeed gtfs;

			using (FileStream stream = File.Open(zipPath, FileMode.Open, FileAccess.Read))
			{
				gtfs = stream.ReadGtfs();
			}

			RunTestsOnGtfs(gtfs);
		}

		[TestMethod]
		[TestProperty("url", "http://www.gtfs-data-exchange.com/agency/jefferson-transit-authority/latest.zip")]
		public void ReadGtfsFromWeb()
		{
			var zipUrl = MethodInfo.GetCurrentMethod().GetCustomAttributes(typeof(TestPropertyAttribute), false).Select(a => (TestPropertyAttribute)a).Where(a => a.Name == "url").First().Value;

			var req = WebRequest.Create(zipUrl) as HttpWebRequest;

			GtfsFeed gtfs = null;

			using (var response = req.GetResponse() as HttpWebResponse)
			{
				Stream stream;

				using (stream = response.GetResponseStream())
				{
					gtfs = stream.ReadGtfs();
				}
			}

			RunTestsOnGtfs(gtfs);

		}

		private static void RunTestsOnGtfs(GtfsFeed gtfs)
		{
			// Test for the different required tables.
			Assert.IsNotNull(gtfs, "The GTFS object cannot be null");
			Assert.IsNotNull(gtfs.Agency, "The agency list cannot be null.");
			Assert.IsNotNull(gtfs.Stops, "The stops list cannot be null.");
			Assert.IsNotNull(gtfs.Routes, "The routes list cannot be null.");
			Assert.IsNotNull(gtfs.Trips, "The trips list cannot be null.");
			Assert.IsNotNull(gtfs.StopTimes, "The stop_times list cannot be null.");
			Assert.IsNotNull(gtfs.Calendar, "The calendar list cannot be null.");

			// Test agency
			foreach (var agency in gtfs.Agency)
			{
				Assert.IsNotNull(agency.agency_name, "agency.agency_name cannot be null.");
				Assert.IsNotNull(agency.agency_url, "agency.agency_url cannot be null.");
				Assert.IsNotNull(agency.agency_timezone, "agency.agency_timezone cannot be null.");
			}

			foreach (var stop in gtfs.Stops)
			{
				Assert.IsNotNull(stop.stop_id, "stop.stop_id cannot be null.");
				Assert.IsNotNull(stop.stop_name, "stop.stop_name cannot be null.");
				// Test to see if the lat is a "valid WGS 84 latitude".
				Assert.IsTrue(stop.stop_lat.IsValidLatitude());
				Assert.IsTrue(stop.stop_lon.IsValidLongitude());
			}

			foreach (var route in gtfs.Routes)
			{
				Assert.IsNotNull(route.route_id, "route.route_id cannot be null.");
				Assert.IsNotNull(route.route_short_name, "route.route_short_name cannot be null.");
				Assert.IsNotNull(route.route_long_name, "route.route_long_name cannot be null.");
			}

			CollectionAssert.AllItemsAreUnique(gtfs.Trips.Select(g => g.trip_id).ToArray());
			foreach (var trip in gtfs.Trips)
			{
				Assert.IsNotNull(trip.route_id, "trip.route_id cannot be null.");
				Assert.IsNotNull(trip.service_id, "trip.service_id cannot be null.");
				Assert.IsNotNull(trip.trip_id, "trip.trip_id cannot be null.");
			}

			foreach (var stopTime in gtfs.StopTimes)
			{
				Assert.IsNotNull(stopTime.trip_id, "stopTime.trip_id cannot be null.");
				Assert.IsNotNull(stopTime.stop_id, "stopTime.stop_id cannot be null.");
				Assert.IsNotNull(stopTime.stop_sequence, "stopTime.stop_sequence cannot be null.");
			}

			CollectionAssert.AllItemsAreUnique(gtfs.Calendar.Select(g => g.service_id).ToArray());
			foreach (var item in gtfs.Calendar)
			{
				Assert.IsNotNull(item.service_id, "item.service_id cannot be null.");
				Assert.IsNotNull(item.start_date, "item.start_date cannot be null.");
				Assert.IsNotNull(item.end_date, "item.end_date cannot be null.");
			}

			if (gtfs.CalendarDates != null)
			{
				CollectionAssert.AllItemsAreUnique(gtfs.CalendarDates.Select(g => string.Concat(g.service_id, g.date)).ToArray(), "Each (service_id, date) pair can only appear once in calendar_dates.txt.");
				foreach (var item in gtfs.CalendarDates)
				{
					Assert.IsNotNull(item.service_id, "item.service_id cannot be null.");
					Assert.IsNotNull(item.date, "item.date cannot be null.");
				}
			}

			if (gtfs.FareAttributes != null)
			{
				CollectionAssert.AllItemsAreUnique(gtfs.FareAttributes.Select(g => g.fare_id).ToArray());
				foreach (var item in gtfs.FareAttributes)
				{
					Assert.IsNotNull(item.fare_id, "item.fare_id cannot be null.");
					Assert.IsTrue(item.price >= 0);
					Assert.IsTrue(!item.transfers.HasValue || (item.transfers.Value.IsInRange(0, 2)), "Number of transfers must be between 0 and 2 if provided at all.");
				}
			}

			if (gtfs.FareRules != null)
			{
				CollectionAssert.AllItemsAreNotNull(gtfs.FareRules.Select(g => g.fare_id).ToArray(), "FareRule.fare_id cannot be null.");
			}

			if (gtfs.Shapes != null)
			{
				foreach (var item in gtfs.Shapes)
				{
					Assert.IsNotNull(item.shape_id, "shape_id cannot be null.");
					Assert.IsTrue(item.shape_pt_lat.IsValidLatitude(), string.Format("{0} is not a valid latitude.", item.shape_pt_lat));
					Assert.IsTrue(item.shape_pt_lon.IsValidLongitude(), string.Format("{0} is not a valid longitude.", item.shape_pt_lon));
					Assert.IsTrue(item.shape_pt_sequence >= 0, "Shape.shape_pt_sequence cannot be a negative number.");
				}
			}
		}
	}
}
