using System;
using System.Windows.Data;

namespace CommonObjects
{
	[ValueConversion(typeof(int), typeof(int))]
	public class PageNumberConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is int)
			{
				return (int)value + 1;
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
