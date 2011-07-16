using System;
using System.Windows.Data;

namespace CommonObjects
{
	//вроде, нигде не используется
	[ValueConversion(typeof(int), typeof(int))]
	public class InvertConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is PolygonSelector)
			{
				return ((PolygonSelector)value).HasProblems;
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
