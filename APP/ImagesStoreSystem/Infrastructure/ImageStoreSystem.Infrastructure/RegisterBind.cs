using System;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Composite.Events;

namespace ImageStoreSystem.Infrastructure
{
    public class RegisterBind 
    {
        public static void Activate(IRegion menuRegion,object menuItem,IRegion contentRegion, object content)
        {      
			ActivateWithCondition(menuRegion, menuItem,
				() => contentRegion.Activate(content));
        }

		public static void ActivateWithCondition(IRegion menuRegion, object menuItem, Action activateAction)
		{
			menuRegion.ActiveViews.CollectionChanged += (s, e) =>
			{
				if (e.NewItems != null)
				{
					if (e.NewItems.Contains(menuItem))
					{
						try
						{
							activateAction();
						}
						catch (Exception ex)
						{
							throw new Exception(String.Format("Activate view exception: {0}\n{1}", ex.Message, ex.StackTrace));
						}
					}
				}
			};
		}
    }
}
