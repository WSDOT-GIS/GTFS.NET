using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Wsdot.Gtfs.Contract;
using Wsdot.Gtfs.IO.ClassMap;

namespace Wsdot.Gtfs.IO
{
	/// <summary>
	/// Reads a GTFS ZIP archive of CSV files.
	/// </summary>
	public static class GtfsReader
	{

		static List<T> ParseCsv<T>(this Stream stream) where T : class, new()
		{
			List<T> list = new List<T>();


			using (var streamReader = new StreamReader(stream, UTF8Encoding.UTF8))
			{
				var csvReader = new CsvReader(streamReader);
				csvReader.Configuration.WillThrowOnMissingField = false;
				// Add maps for types.
				Type t = typeof(T);
				if (t == typeof(Calendar))
				{
					csvReader.Configuration.RegisterClassMap<CalendarMap>();
				}
				else if (t == typeof(CalendarDate))
				{
					csvReader.Configuration.RegisterClassMap<CalendarDateMap>();
				}
				else if (t == typeof(Route))
				{
					csvReader.Configuration.RegisterClassMap<RouteMap>();
				}
				else if (t == typeof(StopTime))
				{
					csvReader.Configuration.RegisterClassMap<StopTimeMap>();
				}

				try
				{
					list = csvReader.GetRecords<T>().ToList();
				}
				catch (Exception ex)
				{
					System.Diagnostics.Trace.TraceError("Error getting records.\t{0}\n{1}", typeof(T), ex);
					throw;
				}
			}

			return list;
		}

		/// <summary>
		/// Parses a CSV file inside of a zip file.
		/// </summary>
		/// <typeparam name="T">The type of data that the CSV contains.</typeparam>
		/// <param name="zip">The zip archive</param>
		/// <param name="fileName">The name of the file inside of <paramref name="zip"/>.</param>
		/// <param name="required">
		/// Indicates if the file specified by <paramref name="fileName"/> is required to be in <paramref name="zip"/>.
		/// If this is set to <see langword="true"/> and the file is not found inside the zip, a <see cref="FileNotFoundException"/> will be thrown.
		/// </param>
		/// <exception cref="FileNotFoundException">
		/// Thrown if <paramref name="zip"/> does not contain a file with a name matching <paramref name="fileName"/>
		/// AND if <paramref name="required"/> is set to <see langword="true"/>.
		/// </exception>
		/// <returns>A list of <typeparamref name="T"/> objects.</returns>
		static List<T> ParseCsv<T>(this ZipArchive zip, string fileName, bool required=false) where T : class, new()
		{

			List<T> list = null;

			var zipEntry = zip.Entries.FirstOrDefault(e => e.Name == fileName);

			if (zipEntry != null)
			{
				using (var agencyStream = zipEntry.Open())
				{
					list = agencyStream.ParseCsv<T>();
				}
			}
			else if (required)
			{
				throw new FileNotFoundException(string.Format("Required file not found in ZIP file: {0}.", fileName), fileName);
			}

			return list;
		}

		/// <summary>
		/// Reads a GTFS zip archive stream and converts it into a <see cref="GtfsFeed"/>.
		/// </summary>
		/// <param name="stream">A zip archive <see cref="Stream"/> containing General Transit Feet Specification data.</param>
		/// <returns></returns>
		public static GtfsFeed ReadGtfs(this Stream stream)
		{
			var zip = new System.IO.Compression.ZipArchive(stream, ZipArchiveMode.Read, false);

			var feed = new GtfsFeed
			{
				Agency = zip.ParseCsv<Agency>("agency.txt", true),
				Stops = zip.ParseCsv<Stop>("stops.txt", true),
				Routes = zip.ParseCsv<Route>("routes.txt", true),
				Trips = zip.ParseCsv<Trip>("trips.txt", true),
				StopTimes = zip.ParseCsv<StopTime>("stop_times.txt", true),
				Calendar = zip.ParseCsv<Calendar>("calendar.txt", true),
				CalendarDates = zip.ParseCsv<CalendarDate>("calendar_dates.txt"),
				FareAttributes = zip.ParseCsv<FareAttribute>("fare_attributes.txt"),
				FareRules = zip.ParseCsv<FareRule>("fare_rules.txt"),
				Shapes = zip.ParseCsv<Shape>("shapes.txt"),
				Frequencies = zip.ParseCsv<Frequency>("frequencies.txt"),
				Transfers = zip.ParseCsv<Transfer>("transfers.txt"),
			};

			var feedInfo = zip.ParseCsv<FeedInfo>("feed_info.txt");
			feed.FeedInfo = feedInfo != null ? feedInfo.FirstOrDefault() : null;

			return feed;
		}
	}
}
