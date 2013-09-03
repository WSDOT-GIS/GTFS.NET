using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtfs.Contract
{
	/// <summary>
	/// The field contains a value that maps to a single distinct entity within the column. 
	/// For example, if a route is assigned the ID 1A, then no other route may use that route ID. 
	/// However, you may assign the ID 1A to a location because locations are a different type 
	/// of entity than routes.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class UniqueAttribute: Attribute
	{
	}
}
