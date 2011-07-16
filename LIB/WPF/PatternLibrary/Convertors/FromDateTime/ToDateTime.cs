using System;
using System.Windows.Data;

namespace WPF.Patterns.Convertors
{
	public class DateTimeToLocalTimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null) return null;
			if (!(value is DateTime)) throw new InvalidOperationException();
			var utcDate = (DateTime)value;
			return utcDate.ToLocalTime();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (!(value is DateTime)) throw new InvalidOperationException();
			var localDate = (DateTime)value;
			return localDate.ToUniversalTime();
		}
	}
}
