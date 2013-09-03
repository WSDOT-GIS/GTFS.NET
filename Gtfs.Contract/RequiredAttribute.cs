using System;

namespace Wsdot.Gtfs.Contract
{
	/// <summary>
	/// The field column must be included in your feed, and a value must be provided for each record. 
	/// Some required fields permit an empty string as a value. To enter an empty string, just omit 
	/// any text between the commas for that field. Note that 0 is interpreted as "a string of value 0", 
	/// and is not an empty string. Please see the field definition for details.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class RequiredAttribute: Attribute
	{
	}
}
