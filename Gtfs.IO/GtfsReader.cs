using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using Gtfs.Contract;
using System.Diagnostics;
using CsvHelper;

namespace Gtfs.IO
{
	public static class GtfsReader
	{
		public static List<Agency> ReadAgencies(this Stream stream)
		{
			List<Agency> agencies = new List<Agency>();

			using (var streamReader = new StreamReader(stream, UTF8Encoding.UTF8))
			{
				var csvReader = new CsvReader(streamReader);
				var agency = new Agency();

				//agencies = csvReader.GetRecords<Agency>().ToList();

				csvReader.Configuration.WillThrowOnMissingField = false;

				while (csvReader.Read())
				{
					string tempStr;
					if (csvReader.TryGetField<string>("agency_id", out tempStr))
					{
						agency.agency_id = tempStr;
					}
					agency.agency_name = csvReader.GetField<string>("agency_name");
					agency.agency_url = csvReader.GetField<string>("agency_url");
					agency.agency_timezone = csvReader.GetField<string>("agency_timezone");

					if (csvReader.TryGetField<string>("agency_lang", out tempStr))
					{
						agency.agency_lang = tempStr;
					}
					if (csvReader.TryGetField<string>("agency_phone", out tempStr))
					{
						agency.agency_phone = tempStr;
					}
					string tempUri;
					if (csvReader.TryGetField<string>("agency_fare_url", out tempUri))
					{
						agency.agency_url = tempUri;
					}
					agencies.Add(agency);
				}
			}

			return agencies;

		}


		/// <summary>
		/// A zip archive <see cref="Stream"/> containing General Transit Feet Specification data.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static Feed ReadGtfs(this Stream stream)
		{
			var zip = new System.IO.Compression.ZipArchive(stream, ZipArchiveMode.Read, false);

			var zipEntry = zip.GetEntry("agency.txt");

			List<Agency> agencies;

			using (var agencyStream = zipEntry.Open())
			{
				agencies = agencyStream.ReadAgencies();
			}

			var feed = new Feed
			{
				Agency = agencies
			};

			// TODO: Read other files from ZIP.

			return feed;
		}
	}
}
