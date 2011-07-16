using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Settings.Infrastucture;
using ImageStoreSystem.Infrastructure;

namespace SettingsModule.Presenters
{
    public class SettingsContentPresenter 
    {
        public SettingsContentPresenter(IEventAggregator eventAggregator)
        {
			Ok = new DelegateCommand<object>(
				o =>
				{
					eventAggregator.GetEvent<SettingsSaveEvent>().Publish(null);
					eventAggregator.GetEvent<DeactivateModalViewEvent>().Publish(null);
				});

			Cancel = new DelegateCommand<object>(
				o =>
				{
					eventAggregator.GetEvent<SettingsReloadEvent>().Publish(null);
					eventAggregator.GetEvent<DeactivateModalViewEvent>().Publish(null);
				});        
        }

        
        public ICommand Ok { get; private set; }
        public ICommand Cancel { get; private set; }
    }
}
