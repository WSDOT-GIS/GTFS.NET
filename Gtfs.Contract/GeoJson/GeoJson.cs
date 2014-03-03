namespace Wsdot.Gtfs.Contract.GeoJson
{
	/// <summary>
	/// Represents a GTFS stop. When serialized to JSON it will become a GeoJSON Feature with a point geometry.
	/// </summary>
	public class StopFeature
	{
		/// <summary>
		/// The type of GeoJSON object. This will always return "Feature".
		/// </summary>
		public string type { get { return "Feature"; } }
		/// <summary>
		/// The point on the Earth where the stop is located.
		/// </summary>
		public Point geometry { get; set; }
		/// <summary>
		/// The properties describing the stop.
		/// </summary>
		public StopFeatureProperties properties { get; set; }
		/// <summary>
		/// The unique identifier of the stop.
		/// </summary>
		[Unique, Required]
		public string id { get; set; }
	}

	/// <summary>
	/// A GeoJSON LineString feature representing a GeoJSON "shape".
	/// </summary>
	public class ShapeFeature
	{
		/// <summary>
		/// GeoJSON object type. This will always return "Feature".
		/// </summary>
		public string type { get { return "Feature"; } }
		/// <summary>
		/// The geometry of the shape.
		/// </summary>
		public LineString geometry { get; set; }
		/// <summary>
		/// The unique identifier of the shape feature.
		/// </summary>
		[Unique, Required]
		public string id { get; set; }
	}

	/// <summary>A GeoJSON FeatureCollection.</summary>
	/// <typeparam name="T">This should be either <see cref="ShapeFeature"/> or <see cref="StopFeature"/>.</typeparam>
	public class FeatureCollection<T> where T: class, new()
	{
		/// <summary>The GeoJSON object type. This will always return "FeatureCollection".</summary>
		public string type { get { return "FeatureCollection"; } }
		/// <summary>The array of GeoJSON features of type <typeparamref name="T"/>.</summary>
		public T[] features { get; set; }
	}

	/// <summary>
	/// Represents a GeoJSON Point.
	/// </summary>
	public class Point
	{
		/// <summary>
		/// The type of GeoJSON object. Always returns "Point".
		/// </summary>
		public string type { get { return "Point"; } }
		/// <summary>
		/// The WGS84 coordinates of the point. An array containing two elements: longitude and latitude, respectively.
		/// </summary>
		public double[] coordinates { get; set; }
	}

	/// <summary>
	/// Represents a GeoJSON LineString.
	/// </summary>
	public class LineString
	{
		/// <summary>
		/// The type of GeoJSON object. This will always return "LineString".
		/// </summary>
		public string type { get { return "LineString"; } }
		/// <summary>
		/// The WGS84 coordinates that make up the line string. Each inner array contains four elements: 
		/// longitude (X), latitude (Y), Z (always null), and distance travelled, respectively.
		/// The measurement unit for the distance travelled value is determined by the GTFS feed publisher.
		/// </summary>
		public double?[][] coordinates { get; set; }
	}


}
