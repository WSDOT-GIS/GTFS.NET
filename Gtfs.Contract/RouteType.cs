using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtfs.Contract
{
	public enum RouteType
	{
		TramStreetcarOrLightRail = 0,
		SubwayOrMetro = 1,
		Rail = 2,
		Bus = 3,
		Ferry = 4,
		CableCar = 5,
		Gondola = 6,
		Funicular = 7
	}
}
