using Microsoft.Practices.Composite.Regions;
using System.Threading;
using System.Windows.Threading;
using System;

namespace ImageStoreSystem.Infrastructure
{
	public class RegionContentPair
	{
		public IRegion Region { get; protected set; }
		public object Content { get; protected set; }
		public String ViewName { get; protected set; }

		public void ActivateView()
		{
			if (Region != null)
			{
				if (Region.Views.Contains(Content))
				{
					Region.Activate(Content);
				}
				else
				{
					throw new System.Exception();
				}
			}
		}

		public void DeactivateView()
		{
			if (Region != null)
			{
				Region.Deactivate(Content);
			}
		}

		public RegionContentPair(IRegion region, object content, string viewName = null)
		{
			Region = region;
			Content = content;
			ViewName = viewName;
		}
	}
}
