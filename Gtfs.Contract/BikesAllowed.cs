using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsdot.Gtfs.Contract
{
	/// <summary>
	/// Indicates if bikes are allowed on a <see cref="Trip"/>.
	/// </summary>
	public enum BikesAllowed
	{
		/// <summary>No information is available.</summary>
		NoInformation = 0,
		/// <summary>
		/// At least one bike is allowed on the <see cref="Trip"/>.
		/// </summary>
		AtLeastOneAllowed = 1,
		/// <summary>
		/// No bikes are allowed on the <see cref="Trip"/>.
		/// </summary>
		NoBikesAllowed = 2
	}
}
