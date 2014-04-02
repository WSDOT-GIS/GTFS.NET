using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Wsdot.Gtfs.Contract;
using Wsdot.Gtfs.Contract.GeoJson;
using Wsdot.Gtfs.IO;

namespace Wsdot.Gtfs.Test
{
	[TestClass]
	public class UnitTest1
	{
		ConsoleTraceListener _consoleListener;

		private TestContext _testContext;

		public TestContext TestContext
		{
			get { return _testContext; }
			set { _testContext = value; }
		}


		[TestInitialize]
		public void Initialize()
		{
			_consoleListener = new ConsoleTraceListener();
			Trace.Listeners.Add(_consoleListener);
		}

		[TestCleanup]
		public void Cleanup()
		{
			Trace.Listeners.Remove(_consoleListener);
		}

		/// <summary>
		/// Test reading a GTFS ZIP file from the file system.
		/// </summary>
		[TestMethod]
		[TestProperty("gtfsFile", "sample-feed.zip")]
		public void ReadSampleGtfs()
		{
			var zipPath = MethodInfo.GetCurrentMethod().GetTestProperty("gtfsFile");

			Assert.IsTrue(File.Exists(zipPath), "File not found: {0}", Path.GetFullPath(zipPath));

			GtfsFeed gtfs = null;

			using (FileStream stream = File.Open(zipPath, FileMode.Open, FileAccess.Read))
			{
				stream.ReadGtfsAsync().ContinueWith(t => gtfs = t.Result).Wait();
			}

			RunTestsOnGtfs(gtfs);
		}

		/// <summary>
		/// Test reading a GTFS ZIP file from the file system.
		/// </summary>
		[TestMethod]
		[TestProperty("gtfsFile", "sample-feed.zip")]
		[TestProperty("options", "Stops,Routes")]
		public void ReadSampleGtfsWithOptions()
		{
			var methodInfo = MethodInfo.GetCurrentMethod();
			var options = (GtfsFileOptions)Enum.Parse(typeof(GtfsFileOptions), methodInfo.GetTestProperty("options"));
			var zipPath = methodInfo.GetTestProperty("gtfsFile");

			Assert.IsTrue(File.Exists(zipPath), "File not found: {0}", Path.GetFullPath(zipPath));

			GtfsFeed gtfs = null;


			using (FileStream stream = File.Open(zipPath, FileMode.Open, FileAccess.Read))
			{
				stream.ReadGtfsAsync(options).ContinueWith(t => gtfs = t.Result).Wait();
			}

			var properties = typeof(GtfsFeed).GetProperties();

			Regex re = new Regex("^((Stops)|(Routes))$", RegexOptions.ExplicitCapture);

			foreach (var p in properties)
			{
				var value = p.GetValue(gtfs);
				if (re.IsMatch(p.Name))
				{
					Assert.IsNotNull(value, "The \"{0}\" property should not be null.", p.Name);
				}
				else
				{
					Assert.IsNull(value, "The \"{0}\" property should be null.", p.Name);
				}
			}
		}

		private async Task<GtfsFeed> ReadGtfsFromWeb(string url = null)
		{
			GtfsFeed gtfs = null;
			using (var httpClient = new HttpClient())
			using (var stream = await httpClient.GetStreamAsync(url))
			{
				stream.ReadGtfsAsync().ContinueWith(t => gtfs = t.Result).Wait();
			}

			RunTestsOnGtfs(gtfs);
			return gtfs;
		}

		/// <summary>
		/// Test downloading a GTFS file from the Internet and parsing it into a GtfsFeed.
		/// </summary>
		[TestMethod]
		[TestProperty("url", "http://www.gtfs-data-exchange.com/agency/jefferson-transit-authority/latest.zip")]
		public void ReadJeffersonGtfsFromWeb()
		{
			var zipUrl = MethodInfo.GetCurrentMethod().GetTestProperty("url");
			ReadGtfsFromWeb(zipUrl).Wait();
		}

		/// <summary>
		/// Test downloading a GTFS file from the Internet and parsing it into a GtfsFeed.
		/// </summary>
		[TestMethod]
		[TestProperty("url", "http://www.gtfs-data-exchange.com/agency/city-of-seattle/latest.zip")]
		public void ReadCityOfSeattleGtfsFromWeb()
		{
			var zipUrl = MethodInfo.GetCurrentMethod().GetTestProperty("url");
			ReadGtfsFromWeb(zipUrl).Wait();
		}

		/// <summary>
		/// Test downloading a GTFS file from the Internet and parsing it into a GtfsFeed.
		/// </summary>
		[TestMethod]
		[TestProperty("url", "http://www.gtfs-data-exchange.com/agency/intercity-transit/latest.zip")]
		public void ReadITFromWeb()
		{
			var zipUrl = MethodInfo.GetCurrentMethod().GetTestProperty("url");
			GtfsFeed gtfs = null;
			ReadGtfsFromWeb(zipUrl).ContinueWith(g => gtfs = g.Result).Wait();
			Assert.IsTrue(gtfs.Shapes.features.Length > 0, "Shapes should not be empty.");
			SerializeGtfs(gtfs, "intercity-transit.json");
		}

		/// <summary>
		/// A common set of tests to run on a GTFS feed.
		/// </summary>
		/// <param name="gtfs"></param>
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

			foreach (var stop in gtfs.Stops.features)
			{
				Assert.IsNotNull(stop.id, "stop.stop_id cannot be null.");
				Assert.IsNotNull(stop.properties.stop_name, "stop.stop_name cannot be null.");
				// Test to see if the lat is a "valid WGS 84 latitude".
				Assert.IsTrue(stop.geometry.coordinates[1].IsValidLatitude());
				Assert.IsTrue(stop.geometry.coordinates[0].IsValidLongitude());
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
				Assert.IsTrue(gtfs.Shapes.features != null && gtfs.Shapes.features.Length > 0,
					"Shapes property is not null but does not contain any features.");
				foreach (var item in gtfs.Shapes.features)
				{
					Assert.IsNotNull(item.id, "shape_id cannot be null.");
					Assert.IsTrue(((ShapeFeature)item).geometry.coordinates.HasValidLongitudesAndLatitudes());
				}
			}
		}

		private void SerializeGtfs(GtfsFeed gtfs, string filename)
		{
			var serializer = new JsonSerializer()
			{
				Formatting = Formatting.Indented,
				NullValueHandling = NullValueHandling.Ignore
			};
			filename = Path.Combine(TestContext.ResultsDirectory, filename);
			Task.Run(() =>
			{
				using (var writer = new StreamWriter(filename))
				{
					serializer.Serialize(writer, gtfs);
				}
			}).Wait();
			TestContext.AddResultFile(filename);
		}

	}
}
