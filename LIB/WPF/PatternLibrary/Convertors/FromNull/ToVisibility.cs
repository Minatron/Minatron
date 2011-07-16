using System;
using System.Windows;
using System.Windows.Data;

namespace WPF.Patterns.Convertors
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class NullToVisibilityConvertor : IValueConverter
    {
        Visibility True;
        Visibility False;
        public NullToVisibilityConvertor(Visibility t, Visibility f)
        {
            True = t;
            False = f;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value == null) ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
