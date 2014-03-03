using System;
using System.Collections.Generic;
using System.Linq;

namespace Wsdot.Gtfs.Contract.GeoJson
{
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
