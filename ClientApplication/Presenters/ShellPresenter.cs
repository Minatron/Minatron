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

            modalViewManager.Show(container.Resolve<Views.ConnectionView>());

            eventAggregator.GetEvent<DisconnectEvent>().Subscribe(
                o =>
                {
                    eventAggregator.GetEvent<ActivateModalViewEvent>().Publish(container.Resolve<Views.LostConnectView>());
                }, true);
        }
    }
}
