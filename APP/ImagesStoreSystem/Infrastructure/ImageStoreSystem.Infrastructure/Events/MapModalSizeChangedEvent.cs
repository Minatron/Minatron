using Microsoft.Practices.Composite.Presentation.Events;

namespace ImageStoreSystem.Infrastructure
{
	public class MapModalSizeChangedEvent : CompositePresentationEvent<MapModalSizeChangedEventArgs>
	{
	}

	public class MapModalSizeChangedEventArgs
	{
		public int MaxWidth { get; protected set; }
		public int MaxHeight { get; protected set; }
		public int MinWidth { get; protected set; }
		public int MinHeight { get; protected set; }

		public MapModalSizeChangedEventArgs(int minWidth, int minHeight, int maxWidth, int maxHeight)
		{
			MaxHeight = maxHeight;
			MaxWidth = maxWidth;
			MinHeight = minHeight;
			MinWidth = minWidth;
		}
	}
}
