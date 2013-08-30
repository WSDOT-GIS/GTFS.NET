using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using Gtfs.IO;
using Gtfs.Contract;
using ServiceStack.Text;
using System.Linq;

namespace Gtfs.Test
{
	[TestClass]
	public class UnitTest1
	{

		private TestContext _testContext;

		public TestContext TestContext
		{
			get { return _testContext; }
			set { _testContext = value; }
		}


		[TestMethod]
		public void ReadSampleGtfs()
		{
			string zipPath = "sample-feed.zip";

			Assert.IsTrue(File.Exists(zipPath), "File not found: {0}", Path.GetFullPath(zipPath));

			GtfsFeed gtfs;

			using (FileStream stream = File.Open(zipPath, FileMode.Open, FileAccess.Read))
			{
				gtfs = stream.ReadGtfs();
			}

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
				Assert.IsNotNull(agency.agency_name);
				Assert.IsNotNull(agency.agency_url);
				Assert.IsNotNull(agency.agency_timezone);
			}

			foreach (var stop in gtfs.Stops)
			{
				Assert.IsNotNull(stop.stop_id);
				Assert.IsNotNull(stop.stop_name);
				// Test to see if the lat is a "valid WGS 84 latitude".
				Assert.IsTrue(stop.stop_lat >= -90 && stop.stop_lat <= 90);
				Assert.IsTrue(stop.stop_lon >= -180 && stop.stop_lon <= 180);
			}

			foreach (var route in gtfs.Routes)
			{
				Assert.IsNotNull(route.route_id);
				Assert.IsNotNull(route.route_short_name);
				Assert.IsNotNull(route.route_long_name);
			}

			CollectionAssert.AllItemsAreUnique(gtfs.Trips.Select(g => g.trip_id).ToArray());
			foreach (var trip in gtfs.Trips)
			{
				Assert.IsNotNull(trip.route_id);
				Assert.IsNotNull(trip.service_id);
				Assert.IsNotNull(trip.trip_id);
			}

			foreach (var stopTime in gtfs.StopTimes)
			{
				Assert.IsNotNull(stopTime.trip_id);
				Assert.IsNotNull(stopTime.arrival_time);
				Assert.IsNotNull(stopTime.departure_time);
				Assert.IsNotNull(stopTime.stop_id);
				Assert.IsNotNull(stopTime.stop_sequence);
			}

			CollectionAssert.AllItemsAreUnique(gtfs.Calendar.Select(g => g.service_id).ToArray());
			foreach (var item in gtfs.Calendar)
			{
				Assert.IsNotNull(item.service_id);
				Assert.IsNotNull(item.start_date);
				Assert.IsNotNull(item.end_date);
			}

			CollectionAssert.AllItemsAreUnique(gtfs.CalendarDates.Select(g => string.Concat(g.service_id, g.date)).ToArray(), "Each (service_id, date) pair can only appear once in calendar_dates.txt.");
			foreach (var item in gtfs.CalendarDates)
			{
				Assert.IsNotNull(item.service_id);
				Assert.IsNotNull(item.date);
			}
		}
	}
}
