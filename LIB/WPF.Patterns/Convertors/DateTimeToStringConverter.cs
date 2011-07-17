using System;
using System.Globalization;
using System.Windows.Data;

namespace Band.WPF.Convertors
{
    public class DateTimeToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return((DateTime) value).ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Parse((string)value);
        }
    }

    public class DateTimeToStringConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString("dd.MM.yyyy HH:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Parse((string)value);
        }
    }
}
