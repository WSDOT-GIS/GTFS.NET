
namespace Gtfs.Test
{
	public static class Extensions
	{

		public static bool IsInRange(this int value, int min, int max)
		{
			return value >= min && value <= max;
		}

		public static bool IsInRange(this double value, double min, double max)
		{
			return value >= min && value <= max;
		}

		public static bool IsValidLatitude(this double latitude)
		{
			return latitude.IsInRange(-90, 90);
		}

		public static bool IsValidLongitude(this double longitude)
		{
			return longitude.IsInRange(-180, 180);
		}
	}
}
