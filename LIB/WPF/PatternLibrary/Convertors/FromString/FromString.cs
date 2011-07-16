using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPF.Patterns.Convertors
{
    public class String
    {
        public static IValueConverter ToDouble = new StringToDoubleConverter();
    }
}
