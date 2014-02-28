﻿using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Wsdot.Gtfs.Contract;
using Wsdot.Gtfs.IO.ClassMap;
using Wsdot.Gtfs.Contract.GeoJson;

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
		/// <param name="options">
		/// Use this to specify which files you want to extract from the GTFS. Defaults to <see cref="GtfsFileOptions.All"/>.
		/// You should specify this parameter if you know that you will only need certain tables out of the GTFS.
		/// </param>
		/// <returns>A <see cref="GtfsFeed"/> representation of the contents of <paramref name="stream"/>.</returns>
		public static GtfsFeed ReadGtfs(this Stream stream, GtfsFileOptions options=GtfsFileOptions.All)
		{
			var zip = new System.IO.Compression.ZipArchive(stream, ZipArchiveMode.Read, false);

			if (options == GtfsFileOptions.None)
			{
				throw new ArgumentException("The \"options\" parameter cannot be \"None\".");
			}

			var feed = new GtfsFeed
			{
				Agency = options.HasFlag(GtfsFileOptions.Agency) ? zip.ParseCsv<Agency>("agency.txt", true) : null,
				Stops =  options.HasFlag(GtfsFileOptions.Stops) ? zip.ParseCsv<Stop>("stops.txt", true).ToFeatureCollection() : null,
				Routes = options.HasFlag(GtfsFileOptions.Routes) ? zip.ParseCsv<Route>("routes.txt", true) : null,
				Trips = options.HasFlag(GtfsFileOptions.Trips) ? zip.ParseCsv<Trip>("trips.txt", true) : null,
				StopTimes =options.HasFlag(GtfsFileOptions.StopTimes) ?  zip.ParseCsv<StopTime>("stop_times.txt", true) : null,
				Calendar = options.HasFlag(GtfsFileOptions.Calendar) ? zip.ParseCsv<Calendar>("calendar.txt", true) : null,
				CalendarDates = options.HasFlag(GtfsFileOptions.CalendarDates) ? zip.ParseCsv<CalendarDate>("calendar_dates.txt") : null,
				FareAttributes = options.HasFlag(GtfsFileOptions.FareAttributes) ? zip.ParseCsv<FareAttribute>("fare_attributes.txt") : null,
				FareRules = options.HasFlag(GtfsFileOptions.FareRules) ? zip.ParseCsv<FareRule>("fare_rules.txt") : null,
				Shapes = options.HasFlag(GtfsFileOptions.Shapes) ? zip.ParseCsv<Shape>("shapes.txt").ToFeatureCollection() : null,
				Frequencies = options.HasFlag(GtfsFileOptions.Frequencies) ? zip.ParseCsv<Frequency>("frequencies.txt") : null,
				Transfers = options.HasFlag(GtfsFileOptions.Transfers) ? zip.ParseCsv<Transfer>("transfers.txt") : null
			};

			var feedInfo = options.HasFlag(GtfsFileOptions.FeedInfo) ? zip.ParseCsv<FeedInfo>("feed_info.txt") : null;
			feed.FeedInfo = feedInfo != null ? feedInfo.FirstOrDefault() : null;

			return feed;
		}
	}


}
