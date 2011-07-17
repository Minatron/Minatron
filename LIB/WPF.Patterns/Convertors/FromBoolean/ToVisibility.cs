using System;
using System.Windows;
using System.Windows.Data;

namespace Band.WPF.Convertors
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConvertor : IValueConverter
    {
        Visibility True;
        Visibility False;
        public BooleanToVisibilityConvertor(Visibility t,Visibility f)
        {
            True =t;
            False = f;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool)value) ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

