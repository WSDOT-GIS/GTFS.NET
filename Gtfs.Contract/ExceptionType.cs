
namespace Wsdot.Gtfs.Contract
{
	/// <summary>
	/// Indicates whether service is available on a specified date.
	/// </summary>
	public enum ExceptionType
	{
		/// <summary>
		/// Service has been added for the specified date.
		/// </summary>
		Added = 1,
		/// <summary>
		/// Service has been removed for the specified date.
		/// </summary>
		Removed = 2
	}
}
