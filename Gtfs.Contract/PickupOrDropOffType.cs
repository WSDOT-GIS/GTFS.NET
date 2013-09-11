
namespace Wsdot.Gtfs.Contract
{
	/// <summary>
	/// Indicates the type of pickup or drop off.
	/// </summary>
	public enum PickupOrDropOffType
	{
		/// <summary>
		/// Regularly scheduled pickup / drop off.
		/// </summary>
		RegularlyScheduled = 0,
		/// <summary>
		/// No pickup / drop off available.
		/// </summary>
		NoneAvailable = 1,
		/// <summary>
		/// Must phone agency to arrange pickup / drop off.
		/// </summary>
		MustPhoneAgencyToArrange = 2,
		/// <summary>
		/// Must coordinate with driver to arrange pickup / drop off.
		/// </summary>
		MustCoordinateWithDriverToArrange = 3
	}
}
