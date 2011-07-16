using System.Windows;
using System.Windows.Data;
namespace WPF.Patterns.Convertors
{
    public static class True
    {
        public new static readonly IValueConverter ToString = null;
        public static readonly IValueConverter ToCollapsed = new BooleanToVisibilityConvertor(Visibility.Collapsed,Visibility.Visible);
        public static readonly IValueConverter ToHidden = new BooleanToVisibilityConvertor(Visibility.Hidden, Visibility.Visible);
        public static readonly IValueConverter ToWidthStar = new BooleanToWidthStar(new GridLength(2,GridUnitType.Star));
        public static readonly IValueConverter ToWidthAuto = new BooleanToWidthStar(GridLength.Auto);

    }

    public static class False
    {
        public new static readonly IValueConverter ToString = True.ToString;
        public static readonly IValueConverter ToCollapsed = new BooleanToVisibilityConvertor(Visibility.Visible, Visibility.Collapsed);
        public static readonly IValueConverter ToHidden = new BooleanToVisibilityConvertor(Visibility.Visible, Visibility.Hidden);

    }
}
