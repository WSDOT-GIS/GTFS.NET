using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using Gtfs.Contract;

namespace Gtfs.IO
{
	public static class GtfsReader
	{
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
				agencies = ServiceStack.Text.CsvSerializer.DeserializeFromStream<List<Agency>>(agencyStream);
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
