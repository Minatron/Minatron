using System.Windows;
using System.Windows.Data;
namespace Band.WPF.Convertors
{
    public static class True
    {
        public new static readonly IValueConverter ToString = null;
        public static readonly IValueConverter ToCollapsed = new BooleanToVisibilityConvertor(Visibility.Collapsed,Visibility.Visible);
        public static readonly IValueConverter ToHidden = new BooleanToVisibilityConvertor(Visibility.Hidden, Visibility.Visible);
    }

    public static class False
    {
        public new static readonly IValueConverter ToString = True.ToString;
        public static readonly IValueConverter ToCollapsed = new BooleanToVisibilityConvertor(Visibility.Visible, Visibility.Collapsed);
        public static readonly IValueConverter ToHidden = new BooleanToVisibilityConvertor(Visibility.Visible, Visibility.Hidden);
    }
}
