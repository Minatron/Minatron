using System.Windows;
using System.Windows.Data;
using WPF.Patterns.Convertors.FromInt;

namespace WPF.Patterns.Convertors
{
	public static class NotZero
	{
		public static readonly IValueConverter ToCollapsed = new ZeroToVisibilityConvertor(Visibility.Visible, Visibility.Collapsed);
		public static readonly IValueConverter ToHidden = new ZeroToVisibilityConvertor(Visibility.Visible, Visibility.Hidden);
	}

	public static class Zero
	{
		public static readonly IValueConverter ToCollapsed = new ZeroToVisibilityConvertor(Visibility.Collapsed, Visibility.Visible);
		public static readonly IValueConverter ToHidden = new ZeroToVisibilityConvertor(Visibility.Hidden, Visibility.Visible);
	}
}
