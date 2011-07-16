using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPF.Patterns.Convertors.FromMath
{
    [ValueConversion(typeof(double), typeof(double))]
    public class DoubleToNegativeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return  -(double) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -(double)value;
        }
    }
    
}
