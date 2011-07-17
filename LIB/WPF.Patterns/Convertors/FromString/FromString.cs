using System.Windows.Data;

namespace Band.WPF.Convertors
{
    public class String
    {
        public static IValueConverter ToDouble = new StringToDoubleConverter();
    }
}
