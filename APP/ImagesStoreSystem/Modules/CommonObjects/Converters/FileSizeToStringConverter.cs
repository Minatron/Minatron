using System;
using System.Windows.Data;
using LocalizationLibrary;

namespace CommonObjects
{
	[ValueConversion(typeof(long), typeof(String))]
	public class FileSizeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is long)
			{
				long size = (long)value;
				if (size > 1024 * 1024)
				{
					return String.Format("{0:0.##} {1}", size / (1024 * 1024), Lang.GetTitle("FileSizes/Mb"));
				}
				if (size > 1024)
				{
                    return String.Format("{0:0.##} {1}", size / 1024, Lang.GetTitle("FileSizes/Kb"));
				}
                return String.Format("{0:0.##} {1}", size, Lang.GetTitle("FileSizes/b"));
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
