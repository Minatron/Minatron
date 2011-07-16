using System;
using System.Windows.Data;
using ImagesStoreSystem.DBProvider.Core;

namespace CommonObjects
{
	[ValueConversion(typeof(long), typeof(string))]
	public class StationNumberConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			NumberedDictionaryRepository<Station> repo = parameter as NumberedDictionaryRepository<Station>;
			if (repo != null && value is long)
			{
				var station = repo.GetBy((long)value);
				if (station == null || String.IsNullOrWhiteSpace(station.Title))
				{
					return String.Format(@"№{0}", value);
				}
				return String.Format(@"{0} (№{1})", station.Title, value);
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
