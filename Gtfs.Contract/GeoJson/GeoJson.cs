using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

	/// <summary>
	/// Provides extension methods.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Converts an enumeration of <see cref="Shape"/> objects into a <see cref="FeatureCollection&lt;T&gt;"/>
		/// of <see cref="ShapeFeature"/> objects.
		/// </summary>
		/// <param name="shapes">A list of <see cref="Shape"/> objects.</param>
		/// <returns>
		/// Returns a <see cref="FeatureCollection&lt;T&gt;"/> containing <see cref="ShapeFeature">ShapeFeatures</see>.
		/// Returns <see langword="null"/> if all of the objects in <paramref name="shapes"/> are <see langword="null"/>.
		/// </returns>
		public static FeatureCollection<ShapeFeature> ToFeatureCollection(this IEnumerable<Shape> shapes)
		{
			var shapeGroups = shapes.GroupBy(shape => shape.shape_id).Select(sg => sg.ToShapeFeature());
			if (shapeGroups.Count(sg => sg != null) < 1)
			{
				return null;
			}
			else
			{
				return new FeatureCollection<ShapeFeature>
				{
					features = shapeGroups.ToArray()
				};
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="shapes">A group of <see cref="Shape"/> keyed with a common <see cref="Shape.shape_id"/>.</param>
		/// <returns></returns>
		public static ShapeFeature ToShapeFeature(this IGrouping<string, Shape> shapes)
		{
			if (shapes.Count() < 1)
			{
				return null;
			}
			var shapeFeature = new ShapeFeature
			{
				id = shapes.Key,
				geometry = new LineString
				{
					coordinates = shapes.OrderBy(k => k.shape_pt_sequence).Select(s => new[] { 
						s.shape_pt_lon, 
						s.shape_pt_lat, 
						default(double?), // There's no z value, so set this to null.
						s.shape_dist_traveled.HasValue ? Convert.ToDouble(s.shape_dist_traveled.Value) : default(double?) 
					}).ToArray()
				}
			};

			return shapeFeature;
		}

		/// <summary>
		/// Converts a collection of <see cref="Stop">Stops</see> into a <see cref="FeatureCollection&lt;T&gt;"/>.
		/// </summary>
		/// <param name="stops">A collection of <see cref="Stop">Stops</see>.</param>
		/// <returns>Returns a <see cref="FeatureCollection&lt;T&gt;"/> containing <see cref="StopFeature">StopFeatures</see>.</returns>
		public static FeatureCollection<StopFeature> ToFeatureCollection(this IEnumerable<Stop> stops)
		{
			return new FeatureCollection<StopFeature> { features = stops.Select(stop => stop.ToStopFeature()).ToArray() };
		}

		/// <summary>
		/// Converts a <see cref="Stop"/> to <see cref="StopFeatureProperties"/>.
		/// </summary>
		/// <param name="stop">A <see cref="Stop"/>.</param>
		/// <returns>Returns a <see cref="StopFeatureProperties"/> object.</returns>
		public static StopFeatureProperties ToProperties(this Stop stop)
		{
			return new StopFeatureProperties
			{
				location_type = stop.location_type,
				parent_station = stop.parent_station,
				stop_code = stop.stop_code,
				stop_desc = stop.stop_desc,
				stop_name = stop.stop_name,
				stop_timezone = stop.stop_timezone,
				stop_url = stop.stop_url,
				wheelchair_boarding = stop.wheelchair_boarding,
				zone_id = stop.zone_id
			};
		}

		/// <summary>
		/// Converts a <see cref="Stop"/> into a <see cref="StopFeature"/>.
		/// </summary>
		/// <param name="stop">A <see cref="Stop"/>.</param>
		/// <returns>A <see cref="StopFeature"/>.</returns>
		public static StopFeature ToStopFeature(this Stop stop)
		{
			return new StopFeature
			{
				id = stop.stop_id,
				geometry = new Point { coordinates = new[] { stop.stop_lon, stop.stop_lat } },
				properties = stop.ToProperties()
			};

		}
	}
}
