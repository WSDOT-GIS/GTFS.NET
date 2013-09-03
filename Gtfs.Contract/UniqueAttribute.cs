using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtfs.Contract
{
	[AttributeUsage(AttributeTargets.Property)]
	public class UniqueAttribute: Attribute
	{
	}
}
