using System.Windows;
using System.Windows.Data;
namespace WPF.Patterns.Convertors
{
    public static class NotNull
    {
        public new static readonly IValueConverter ToString = null;
        public static readonly IValueConverter ToCollapsed = new NullToVisibilityConvertor(Visibility.Visible, Visibility.Collapsed);
        public static readonly IValueConverter ToHidden = new NullToVisibilityConvertor(Visibility.Visible, Visibility.Hidden);
        
    }

    public static class Null
    {
        public new static readonly IValueConverter ToString = True.ToString;
        public static readonly IValueConverter ToCollapsed = new NullToVisibilityConvertor(Visibility.Collapsed, Visibility.Visible);
        public static readonly IValueConverter ToHidden = new NullToVisibilityConvertor(Visibility.Hidden, Visibility.Visible);

    }
}