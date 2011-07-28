using System.Windows.Input;
using Band.Client.Infrastructure;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace Band.Module.WeighData.Presenters
{
    public class WeighDataCamerasPresenter
    {
        readonly ModalViewManager _modalManager;

        public ICommand CloseMeCommand { get; protected set; }

        public WeighDataCamerasPresenter(ModalViewManager modal)
        {
            _modalManager = modal;

            CloseMeCommand = new DelegateCommand<object>(CloseMe);
        }

        public void CloseMe(object obj = null)
        {
            _modalManager.Hide();
        }
    }
}
