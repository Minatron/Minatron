using System;
using System.Windows;
using System.Windows.Data;

namespace Band.WPF.Convertors
{
	[ValueConversion(typeof(int), typeof(Visibility))]
	public class ZeroToVisibilityConvertor : IValueConverter
	{
		Visibility True;
		Visibility False;
		public ZeroToVisibilityConvertor(Visibility t, Visibility f)
		{
			True = t;
			False = f;
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is int) return ((int)value == 0) ? True : False;
			else if (value is long) return ((long)value == 0) ? True : False;
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
