using System.Windows.Input;
using Band.Client.Infrastructure;
using Band.Client.Infrastructure.Events;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace Band.Module.WeighData.Presenters
{
    public class WeighDataCamerasPresenter
    {
        readonly ModalViewManager _modalManager;
        private readonly IEventAggregator _eventAgrigator;

        public ICommand CloseMeCommand { get; protected set; }

        public WeighDataCamerasPresenter(ModalViewManager modal, IEventAggregator eventAgrigator)
        {
            _modalManager = modal;
            _eventAgrigator = eventAgrigator;

            CloseMeCommand = new DelegateCommand<object>(CloseMe);
        }

        public void CloseMe(object obj = null)
        {
            _modalManager.Hide();
            _eventAgrigator.GetEvent<BackButtonPushEvent>().Publish(null);
            
        }
    }
}
