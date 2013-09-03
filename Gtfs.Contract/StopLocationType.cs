
namespace Wsdot.Gtfs.Contract
{
	/// <summary>
	/// The location_type field identifies whether this stop ID represents a stop or station. If no location type is specified, or the location_type is blank, stop IDs are treated as stops. Stations may have different properties from stops when they are represented on a map or used in trip planning.
	/// </summary>
	public enum StopLocationType
	{
		/// <summary>Stop. A location where passengers board or disembark from a transit vehicle.</summary>
		Stop = 0,
		/// <summary>Station. A physical structure or area that contains one or more stop. </summary>
		Station = 1
	}
}
