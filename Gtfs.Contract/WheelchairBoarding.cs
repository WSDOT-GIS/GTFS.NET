
namespace Gtfs.Contract
{
	/// <summary>
	/// Indicates wheelchair accessibility
	/// </summary>
	public enum WheelchairAccessibility
	{
		/// <summary>
		/// Indicates that there is no accessibility information.
		/// </summary>
		NoInformation = 0,
		/// <summary>
		/// Indicates that the vehicle being used can accommodate at least one rider in a wheelchair
		/// </summary>
		SomeAccessibility = 1,
		/// <summary>
		/// Indicates that no riders in wheelchairs can be accommodated
		/// </summary>
		NoAccessibility = 2
	}
}
