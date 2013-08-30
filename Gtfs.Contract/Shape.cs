
namespace Gtfs.Contract
{
	public class Shape
	{
		[Required]
		public string shape_id { get; set; }

		[Required]
		public double shape_pt_lat { get; set; }

		[Required]
		public double shape_pt_lon { get; set; }

		[Required]
		public int shape_pt_sequence { get; set; }

		public float shape_dist_traveled { get; set; }

	}
}
