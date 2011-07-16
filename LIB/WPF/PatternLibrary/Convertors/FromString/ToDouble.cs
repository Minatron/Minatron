using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPF.Patterns.Convertors
{
    public class StringToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var calInfo = CultureInfo.InvariantCulture;
                return ((double)value).ToString("F6", calInfo);
            }
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double res;
            var calInfo = CultureInfo.InvariantCulture;
            return double.TryParse((string)value, NumberStyles.Any, calInfo, out res) ? res : 0;
        }
    }
}
