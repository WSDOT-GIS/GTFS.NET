using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtfs.Contract
{
	public enum PaymentMethod
	{
		FareIsPaidOnBoard = 0,
		FareMustBePaidBeforeBoarding = 1
	}
}
