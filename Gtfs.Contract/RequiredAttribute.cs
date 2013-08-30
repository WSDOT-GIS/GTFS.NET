using System;

namespace Gtfs.Contract
{
	/// <summary>
	/// Indicates a required field in a GTFS CSV table.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class RequiredAttribute: Attribute
	{
	}
}
