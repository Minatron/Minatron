using System.ComponentModel;
using System.Windows.Input;
using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using StorageModule.Services;
using StorageModule.Views;
using WPF.Patterns.Commands;

namespace StorageModule.Presenters
{
	public class StorageSettingsPresenter : INotifyPropertyChanged
    {
		IEventAggregator _eventAggregator;
		IRegionManager _regionManager;
		IUnityContainer _container;
		bool _isFirstConnect = true;
		bool _hasProblems = false;

		public bool HasProblems
		{
			get
			{
				return _hasProblems;
			}
			protected set
			{
				_hasProblems = value;
				OnPropertyChanged("HasProblems");
			}
		}

		public bool IsConnected
		{
			get
			{
				return _container.Resolve<StorageService>().IsConnect;
			}
		}

		public ICommand ConnectCommand { get; protected set; }

		public StorageSettingsPresenter(IUnityContainer container, Properties.Settings settings, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            Settings = settings;
			_eventAggregator = eventAggregator;
			_container = container;
			_regionManager = regionManager;

			eventAggregator.GetEvent<DisconnectEvent>().Subscribe(
				o =>
				{
					_eventAggregator.GetEvent<ActivateModalViewEvent>().Publish(container.Resolve<LostConnectView>());
					OnPropertyChanged("IsConnected");
				}, true);

			ConnectCommand = new DelegateCommand(
				() =>
				{
					var storage = container.Resolve<StorageService>();
					Settings.Save();
					storage.Connect();
					if (_isFirstConnect)
					{
						container.Resolve<ModulesWithConditionDictionary>().InitModulesWithTrueCondition();
					}
					if (storage.IsConnect)
					{
						_isFirstConnect = false;
						_eventAggregator.GetEvent<RefreshAllEvent>().Publish(null);
						_eventAggregator.GetEvent<ShowPreviousView>().Publish(null);
						_eventAggregator.GetEvent<DeactivateModalViewEvent>().Publish(null);
						OnPropertyChanged("IsConnected");
					}
					HasProblems = !storage.IsConnect;
				});
        }

        public Properties.Settings Settings { get; private set; }

		#region INotifyPropertyChanged

		void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null) return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
    }
}
