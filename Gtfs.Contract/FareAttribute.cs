
namespace Gtfs.Contract
{
	/// <summary>
	/// Fare information for a transit organization's routes.
	/// </summary>
	public class FareAttribute
	{
		/// <summary>
		/// The fare_id field contains an ID that uniquely identifies a fare class. The fare_id is dataset unique.
		/// </summary>
		[Required,Unique]
		public string fare_id { get; set; }
		/// <summary>
		/// The price field contains the fare price, in the unit specified by currency_type.
		/// </summary>
		[Required]
		public decimal price { get; set; }
		/// <summary>
		/// The currency_type field defines the currency used to pay the fare.
		/// Please use <see href="http://www.iso.org/iso/home/standards/iso4217.htm">the ISO 4217 alphabetical currency codes</see>.
		/// </summary>
		[Required]
		public string currency_type { get; set; }
		/// <summary>
		/// Indicates when the fare must be paid.
		/// </summary>
		[Required]
		public PaymentMethod payment_method { get; set; }

		/// <summary>
		/// Number of transfers permitted on this fare.  Valid values are 0-2 or <see langword="null"/>.
		/// </summary>
		[Required]
		public int? transfers { get; set; }

		/// <summary>
		/// <para>The transfer_duration field specifies the length of time in seconds before a transfer expires.</para>
		/// <para>When used with a transfers value of 0, the transfer_duration field indicates how long a ticket is valid for a fare where no transfers are allowed. Unless you intend to use this field to indicate ticket validity, transfer_duration should be omitted or empty when transfers is set to 0.</para>
		/// </summary>
		public int? transfer_duration { get; set; }
	}
}
