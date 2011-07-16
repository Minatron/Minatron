using System;
using System.Windows.Data;
using ImagesStoreSystem.DBProvider.Core;

namespace CommonObjects
{
	[ValueConversion(typeof(long), typeof(string))]
	public class SatelliteNumberConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			NumberedDictionaryRepository<Satellite> repo = parameter as NumberedDictionaryRepository<Satellite>;
			if (repo != null && value is long)
			{
				var satellite = repo.GetBy((long)value);
				if (satellite == null || String.IsNullOrWhiteSpace(satellite.Title))
				{
					return String.Format(@"№{0}", value);
				}
				return String.Format(@"{0} (№{1})", satellite.Title, value);
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
