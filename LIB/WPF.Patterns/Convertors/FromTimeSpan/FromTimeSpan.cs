using System.Windows.Data;
namespace WPF.Patterns.Convertors
{
    public static class FromTimeSpan
    {
        public new static readonly IValueConverter ToString = new TimeSpanToStringConvertor();
        public static readonly IValueConverter ToString_Smart = new TimeSpanToStringConvertor(2);
    }
}
