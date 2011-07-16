using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.Practices.Composite.Regions;

namespace ImageStoreSystem.Infrastructure
{
	public class ActivateViewEventArgs
	{
		public bool IsNewSequence {get; protected set;}
		public RegionContentPair RegionContentPair { get; protected set; }

		public ActivateViewEventArgs(RegionContentPair regionContentPair, bool isNewSequence = false)
		{
			IsNewSequence = isNewSequence;
			RegionContentPair = regionContentPair;
		}

		public ActivateViewEventArgs(IRegion region, object content, string viewName = null, bool isNewSequence = false)
			: this(new RegionContentPair(region, content, viewName), isNewSequence)
		{}
	}

	public class ActivateEvent : CompositePresentationEvent<ActivateViewEventArgs>
	{}
}
