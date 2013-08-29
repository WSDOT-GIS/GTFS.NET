using System;

namespace Gtfs.Contract
{
	public class Agency
	{
		/// <summary>
		/// Optional. The agency_id field is an ID that uniquely identifies a transit agency. A transit feed may represent data from more than one agency. The agency_id is dataset unique. This field is optional for transit feeds that only contain data for a single agency.
		/// </summary>
		public string agency_id { get; set; }

		/// <summary>Required. The agency_name field contains the full name of the transit agency. Google Maps will display this name.</summary>
		[Required]
		public string agency_name { get; set; }

		/// <summary>
		/// Required. The agency_url field contains the URL of the transit agency. The value must be a fully qualified URL that includes http:// or https://, and any special characters in the URL must be correctly escaped. See http://www.w3.org/Addressing/URL/4_URI_Recommentations.html for a description of how to create fully qualified URL values.
		/// </summary>
		[Required]
		public string agency_url { get; set; }

		/// <summary>
		/// Required. The agency_timezone field contains the timezone where the transit agency is located. Timezone names never contain the space character but may contain an underscore. Please refer to http://en.wikipedia.org/wiki/List_of_tz_zones for a list of valid values. If multiple agencies are specified in the feed, each must have the same agency_timezone.
		/// </summary>
		[Required]
		public string agency_timezone { get; set; }

		/// <summary>
		/// Optional. The agency_lang field contains a two-letter ISO 639-1 code for the primary language used by this transit agency. The language code is case-insensitive (both en and EN are accepted). This setting defines capitalization rules and other language-specific settings for all text contained in this transit agency's feed. Please refer to http://www.loc.gov/standards/iso639-2/php/code_list.php for a list of valid values.
		/// </summary>
		public string agency_lang { get; set; }

		/// <summary>
		/// Optional. The agency_phone field contains a single voice telephone number for the specified agency. This field is a string value that presents the telephone number as typical for the agency's service area. It can and should contain punctuation marks to group the digits of the number. Dialable text (for example, TriMet's "503-238-RIDE") is permitted, but the field must not contain any other descriptive text.
		/// </summary>
		public string agency_phone { get; set; }

		/// <summary>
		/// Optional. The agency_fare_url specifies the URL of a web page that allows a rider to purchase tickets or other fare instruments for that agency online. The value must be a fully qualified URL that includes http:// or https://, and any special characters in the URL must be correctly escaped. See http://www.w3.org/Addressing/URL/4_URI_Recommentations.html for a description of how to create fully qualified URL values. 
		/// </summary>
		public string agency_fare_url { get; set; }
	}
}
