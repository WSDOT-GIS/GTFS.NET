using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using Gtfs.IO;
using Gtfs.Contract;
using ServiceStack.Text;

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

			Assert.IsNotNull(gtfs, "The GTFS object cannot be null");
			Assert.IsNotNull(gtfs.Agency, "The agency list cannot be null.");
			Assert.IsNotNull(gtfs.Stops, "The stops list cannot be null.");
			Assert.IsNotNull(gtfs.Routes, "The routes list cannot be null.");
			Assert.IsNotNull(gtfs.Trips, "The trips list cannot be null.");
			Assert.IsNotNull(gtfs.StopTimes, "The stop_times list cannot be null.");
			Assert.IsNotNull(gtfs.Calendar, "The calendar list cannot be null.");

		}
	}
}
