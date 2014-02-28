using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsdot.Gtfs.Contract.GeoJson
{
	public class StopFeature
	{
		public string type { get { return "Feature"; } }
		public Point geometry { get; set; }
		public StopFeatureProperties properties { get; set; }
		[Unique, Required]
		public string id { get; set; }
	}

	public class ShapeFeature
	{
		public string type { get { return "Feature"; } }
		public LineString geometry { get; set; }
		[Unique, Required]
		public string id { get; set; }
	}

	public class FeatureCollection<T> where T: class, new()
	{
		public string type { get { return "FeatureCollection"; } }
		public T[] features { get; set; }
	}

	public class Point
	{
		public string type { get { return "Point"; } }
		public double[] coordinates { get; set; }
	}

	public class LineString
	{
		public string type { get { return "LineString"; } }
		public double?[][] coordinates { get; set; }
	}

	public static class Extensions
	{
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

		public static FeatureCollection<StopFeature> ToFeatureCollection(this IEnumerable<Stop> stops)
		{
			return new FeatureCollection<StopFeature> { features = stops.Select(stop => stop.ToStopFeature()).ToArray() };
		}

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
