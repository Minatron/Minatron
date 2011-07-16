using System;
using System.Windows.Controls;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Regions;
using System.Windows.Data;
using System.Windows;

namespace ImageStoreSystem.Infrastructure
{
	public class MainMenuAggregator
	{
		MenuItem _checketItem = null;

		public void RegisterMenu(IRegion region, object menuItemHeader, Action<bool> activateAction, bool isCheckable = true)
		{
			var menu = new Menu();
			var menuItem = new MenuItem() {Header = menuItemHeader};
			Register(menuItem, activateAction, isCheckable);
			menu.Items.Add(menuItem);
			region.Add(menu);
		}

		public MenuItem RegisterMenuItem(IRegion menuRegion, object menuItemHeader, Action<bool> activateAction, bool isCheckable = true)
		{
			var menuItem = new MenuItem() { Header = menuItemHeader };
			menuRegion.Add(menuItem);
			var parent = LogicalTreeHelper.GetParent(menuItem);
			return Register(menuItem, activateAction, isCheckable);
		}

		private MenuItem Register(MenuItem menuItem, Action<bool> activateAction, bool isCheckable)
		{
			if (isCheckable)
			{
				menuItem.Command = new DelegateCommand<object>(
					o =>
					{
						if (o == null)
						{
							if (_checketItem != null)
							{
								_checketItem.Command.Execute(false);
							}
							_checketItem = menuItem;
							_checketItem.IsChecked = true;
							activateAction(true);
						}
						else
						{
							_checketItem.IsChecked = false;
							_checketItem = null;
							activateAction(false);
						}
					});
			}
			else
			{
				menuItem.Command = new DelegateCommand<object>(
					o =>
					{
						activateAction(true);

					});
			}
			return menuItem;
		}
	}
}
