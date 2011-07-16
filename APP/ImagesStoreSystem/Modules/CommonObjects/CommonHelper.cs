using System;

namespace CommonObjects
{
	public static class CommonHelper
	{
		public static DateTime MergeDateTime(DateTime date, DateTime time)
		{
			return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, time.Millisecond);
		}
	}
}
