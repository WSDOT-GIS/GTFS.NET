
namespace Gtfs.Contract
{
	public class Transfer
	{
		[Required]
		public string from_stop_id { get; set; }
		[Required]
		public string to_stop_id { get; set; }
		[Required]
		public TransferType? transfer_type { get; set; }

		public int? min_transfer_time { get; set; }
	}
}
