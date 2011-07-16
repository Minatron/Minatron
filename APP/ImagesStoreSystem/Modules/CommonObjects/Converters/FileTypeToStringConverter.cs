using System;
using System.Windows.Data;
using ImagesStoreSystem.DBProvider.Core;

namespace CommonObjects
{
	[ValueConversion(typeof(FileInfoType), typeof(String))]
	public class FileTypeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is FileInfoType)
			{
				switch((FileInfoType)value)
				{
					case FileInfoType.Default:
						return "";
					default:
						return value.ToString();
				}
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
