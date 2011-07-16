using System;

namespace WPF.Patterns.Extensions
{
	public static class DateTimeExtensions
	{
		public static bool IsSameDate(this DateTime d1, DateTime d2)
		{
			return d1.Date == d2.Date;
		}

		public static bool IsSameDate(this DateTime? d1, DateTime? d2)
		{
			if (!d1.HasValue && !d2.HasValue) return true;
			return (d1.HasValue && d2.HasValue) && d1.Value.IsSameDate(d2.Value);
		}
	}
}
