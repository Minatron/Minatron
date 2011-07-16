using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using WPF.Patterns.Convertors.FromMath;

namespace WPF.Patterns.Convertors
{
    public static class  FromDouble
    {
        public static readonly IValueConverter ToNegative = new DoubleToNegativeConvertor();
        
    }


    public static class FromLong
    {
        public static IValueConverter ToFileSize = new LongToFileSize();
    }

    public class LongToFileSize : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = (long) value;
            if (size / 1073741824 != 0) return (size / 1073741824.0).ToString("F3") + "Gb";
            if (size / 1048576 != 0) return (size / 1048576.0).ToString("F3") + "Mb";
            if (size / 1024 != 0) return (size / 1024.0).ToString("F3") + "Kb";

            return size + "bytes";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
