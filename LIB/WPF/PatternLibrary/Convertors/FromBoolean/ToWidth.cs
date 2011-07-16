using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace WPF.Patterns.Convertors
{
    public class BooleanToWidthStar: IValueConverter
    {
        private GridLength len;

        public BooleanToWidthStar(GridLength _len)
        {
            len = _len;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GridLength.Auto;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return true;
        }
    }
}
