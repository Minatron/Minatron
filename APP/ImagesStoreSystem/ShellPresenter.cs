using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace ImagesStoreSystem
{
	public class ShellPresenter 
	{
		public ViewNavigator Navigator { get; protected set; }
		public ModalViewManager ModalView { get; protected set; }

		public ShellPresenter(IEventAggregator eventAggregator, ModalViewManager modalViewManager)
		{
			Navigator = new ViewNavigator(eventAggregator);
			ModalView = modalViewManager;
		}
    }
}
