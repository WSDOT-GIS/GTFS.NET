using System;

namespace Gtfs.Contract
{
	[AttributeUsage(AttributeTargets.Property)]
	public class RequiredAttribute: Attribute
	{
	}
}
