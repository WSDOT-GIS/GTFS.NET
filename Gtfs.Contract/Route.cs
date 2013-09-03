namespace Gtfs.Contract
{
	/// <summary>
	/// Transit route. A route is a group of <see cref="Trip">trips</see> that are displayed to riders as a single service.
	/// </summary>
	public class Route
	{
		/// <summary>
		/// The route_id field contains an ID that uniquely identifies a route. The route_id is dataset unique.
		/// </summary>
		[Required, Unique]
		public string route_id { get; set; }

		/// <summary>
		/// The agency_id field defines an agency for the specified route. This value is referenced from the agency.txt file.
		/// Use this field when you are providing data for routes from more than one agency.
		/// <seealso cref="Agency.agency_id"/>
		/// </summary>
		public string agency_id { get; set; }

		/// <summary>
		/// The route_short_name contains the short name of a route. 
		/// This will often be a short, abstract identifier like "32", "100X", 
		/// or "Green" that riders use to identify a route, but which doesn't
		/// give any indication of what places the route serves. 
		/// At least one of route_short_name or route_long_name must be specified, 
		/// or potentially both if appropriate. If the route does not have a short name, 
		/// please specify a route_long_name and use an empty string as the value for this field.
		/// </summary>
		[Required]
		public string route_short_name { get; set; }

		/// <summary>
		/// The route_long_name contains the full name of a route. This name is generally more descriptive 
		/// than the route_short_name and will often include the route's destination or stop. At least one 
		/// of route_short_name or route_long_name must be specified, or potentially both if appropriate. 
		/// If the route does not have a long name, please specify a route_short_name and use an empty 
		/// string as the value for this field.
		/// </summary>
		[Required]
		public string route_long_name { get; set; }

		/// <summary>
		///  The route_desc field contains a description of a route. Please provide useful, quality information. 
		///  Do not simply duplicate the name of the route. 
		///  For example, "A trains operate between Inwood-207 St, Manhattan and Far Rockaway-Mott Avenue, Queens 
		///  at all times. Also from about 6AM until about midnight, additional A trains operate between Inwood-207 St 
		///  and Lefferts Boulevard (trains typically alternate between Lefferts Blvd and Far Rockaway)."
		/// </summary>
		public string route_desc { get; set; }

		/// <summary>
		/// Describes the type of transportation used on a route.
		/// <seealso cref="RouteType"/>
		/// </summary>
		[Required]
		public RouteType route_type { get; set; }

		/// <summary>
		/// <para>The route_url field contains the URL of a web page about that particular route.
		/// This should be different from the agency_url.</para>
		/// <para>The value must be a fully qualified URL that includes http:// or https://, 
		/// and any special characters in the URL must be correctly escaped. 
		/// See <see href="http://www.w3.org/Addressing/URL/4_URI_Recommentations.html" /> for a description 
		/// of how to create fully qualified URL values.</para>
		/// </summary>
		public string route_url { get; set; }

		/// <summary>
		/// <para>In systems that have colors assigned to routes,
		///   the <strong>route_color</strong> field defines a color that
		///   corresponds to a route. The color must be provided as a six-character
		///   hexadecimal number, for example, 00FFFF. If no color is specified,
		///   the default route color is white (FFFFFF).</para>
		/// <para>The color difference between <strong>route_color</strong>
		///   and <strong>route_text_color</strong> should provide sufficient
		///   contrast when viewed on a black and white
		///   screen. The <a href="http://www.w3.org/TR/AERT#color-contrast">W3C
		///   Techniques for Accessibility Evaluation And Repair Tools document</a>
		///   offers a useful algorithm for evaluating color contrast. There are
		///   also helpful online tools for choosing contrasting colors, including
		///   the <a href="http://snook.ca/technical/colour_contrast/colour.html">snook.ca
		///   Color Contrast Check application</a>.</para>
		/// </summary>
		public int? route_color { get; set; }

		/// <summary>
		/// <para>The <strong>route_text_color</strong> field can be used to specify a
		///   legible color to use for text drawn against a background
		///   of <strong>route_color</strong>. The color must be provided as a
		///   six-character hexadecimal number, for example, FFD700. If no color is
		///   specified, the default text color is black (000000).</para>
		/// <para>The color difference between <strong>route_color</strong>
		///   and <strong>route_text_color</strong> should provide sufficient
		///   contrast when viewed on a black and white screen.</para>
		/// </summary>
		public int? route_text_color { get; set; }
	}
}
