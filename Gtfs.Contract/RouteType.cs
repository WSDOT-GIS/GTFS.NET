namespace Gtfs.Contract
{
	/// <summary>
	/// Describes the type of transportation used on a route.
	/// </summary>
	public enum RouteType
	{
		/// <summary>
		/// Tram, Streetcar, Light rail. Any light rail or street level system within a metropolitan area. 
		/// </summary>
		TramStreetcarOrLightRail = 0,
		/// <summary>
		/// Subway, Metro. Any underground rail system within a metropolitan area. 
		/// </summary>
		SubwayOrMetro = 1,
		/// <summary>
		/// Rail. Used for intercity or long-distance travel. 
		/// </summary>
		Rail = 2,
		/// <summary>
		/// Bus. Used for short- and long-distance bus routes. 
		/// </summary>
		Bus = 3,
		/// <summary>
		/// Ferry. Used for short- and long-distance boat service. 
		/// </summary>
		Ferry = 4,
		/// <summary>
		/// Cable car. Used for street-level cable cars where the cable runs beneath the car. 
		/// </summary>
		CableCar = 5,
		/// <summary>
		/// Gondola, Suspended cable car. Typically used for aerial cable cars where the car is suspended from the cable. 
		/// </summary>
		Gondola = 6,
		/// <summary>
		/// Funicular. Any rail system designed for steep inclines. 
		/// </summary>
		Funicular = 7
	}
}
