using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtfs.Contract
{
	public enum PickupOrDropOffType
	{
		RegularyScheduled = 0,
		NoneAvailable = 1,
		MustPhoneAgencyToArrange = 2,
		MustCoordinateWithDriverToArrange = 3
	}
}
