using System.ComponentModel;
using System.Windows.Input;
using Band.Client.Infrastructure.Events;
using Band.Client.Infrastructure.Properties;
using Band.Client.Infrastructure.Storage;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace Band.Client.App.Presenters
{
    public class ConnectionPresenter : INotifyPropertyChanged
    {
        StorageService _service;
        bool _hasProblems = false;

        public ICommand ConnectCommand  { get; private set; }

        public ConnectionPresenter(StorageService service, AppSettings settings, IEventAggregator eventAggregator)
        {
            _service = service;

            ConnectCommand = new DelegateCommand<object>(
                (o) =>
                {
                    settings.Save();
                    _service.Connect();
                    if (_service.IsConnect)
                    {
                        eventAggregator.GetEvent<RefreshAllEvent>().Publish(null);
                        eventAggregator.GetEvent<ShowPreviousView>().Publish(null);
                        eventAggregator.GetEvent<DeactivateModalViewEvent>().Publish(null);
                        OnPropertyChanged("IsConnected");
                    }
                    HasProblems = !_service.IsConnect;
                });
        }

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
                return _service.IsConnect;
            }
        }

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
