using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtfs.Contract
{
	public class FareAttribute
	{
		[Required]
		public string fare_id { get; set; }
		[Required]
		public decimal price { get; set; }
		[Required]
		public string currency_type { get; set; }
		[Required]
		public PaymentMethod payment_method { get; set; }
		[Required]
		public int? transfers { get; set; }

		/// <summary>
		/// <para>The transfer_duration field specifies the length of time in seconds before a transfer expires.</para>
		/// <para>When used with a transfers value of 0, the transfer_duration field indicates how long a ticket is valid for a fare where no transfers are allowed. Unless you intend to use this field to indicate ticket validity, transfer_duration should be omitted or empty when transfers is set to 0.</para>
		/// </summary>
		public int? transfer_duration { get; set; }
	}
}
