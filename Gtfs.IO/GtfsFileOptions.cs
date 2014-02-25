using System;

namespace Wsdot.Gtfs.IO
{
	/// <summary>
	/// This <see langword="enum"/> is used to specify which files will be extracted from a GTFS file.
	/// </summary>
	[Flags]
	public enum GtfsFileOptions
	{
		///<summary>None</summary>
		None = 0,
		///<summary>Agency</summary>
		Agency = 1,
		///<summary>Stops</summary>
		Stops = 2,
		///<summary>Routes</summary>
		Routes = 4,
		///<summary>Trips</summary>
		Trips = 8,
		///<summary>StopTimes</summary>
		StopTimes = 0x10,
		///<summary>Calendar</summary>
		Calendar = 0x20,
		///<summary>CalendarDates</summary>
		CalendarDates = 0x40,
		///<summary>FareAttributes</summary>
		FareAttributes = 0x80,
		///<summary>FareRules</summary>
		FareRules = 0x100,
		///<summary>Shapes</summary>
		Shapes = 0x200,
		///<summary>Frequencies</summary>
		Frequencies = 0x400,
		///<summary>Transfers</summary>
		Transfers = 0x800,
		/// <summary>Feed Info</summary>
		FeedInfo = 0x1600,
		/// <summary>All of the tables will be examined and extracted.</summary>
		All = Agency | Stops | Routes | Trips | StopTimes | Calendar | CalendarDates | FareAttributes | FareRules | Shapes | Frequencies | Transfers | FeedInfo
	}
}
