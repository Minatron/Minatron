using System.Windows.Controls;
using System.Windows.Data;

namespace DictionaryModule.Views
{
	public partial class MenuHeaderView
	{
		public MenuHeaderView(object title)
		{
			InitializeComponent();
			var binding = title as Binding;
			if (binding != null)
			{
				SetBinding(TextBlock.TextProperty, binding);
			}
			else
			{
				Text = title.ToString();
			}
		}
	}
}
