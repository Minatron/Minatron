using Band.Client.Infrastructure;
using Band.Client.Infrastructure.Events;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Unity;

namespace Band.Client.App.Presenters
{
    public class ShellPresenter
    {
        public ModalViewManager ModalView { get; protected set; }
       
        public ShellPresenter(IUnityContainer container,IEventAggregator eventAggregator, ModalViewManager modalViewManager)
        {
            ModalView = modalViewManager;

            ModalView.Show(container.Resolve<Views.ConnectionView>(),ModalViewType.Center);

            eventAggregator.GetEvent<DisconnectEvent>().Subscribe(
                o =>
                {
                    ModalView.Show(container.Resolve<Views.LostConnectView>(), ModalViewType.Center);
                }, true);
        }
    }
}
