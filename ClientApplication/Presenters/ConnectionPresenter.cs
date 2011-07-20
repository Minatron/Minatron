using System.ComponentModel;
using System.Windows.Input;
using Band.Client.Infrastructure.Events;
using Band.Client.Infrastructure.Properties;
using Band.Client.Infrastructure.Storage;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Band.Client.Infrastructure;

namespace Band.Client.App.Presenters
{
    public class ConnectionPresenter : INotifyPropertyChanged
    {
        StorageService  _service;
        AppSettings     _settings;
        bool _hasProblems = false;

        public ICommand ConnectCommand  { get; private set; }

        public ConnectionPresenter(StorageService service, AppSettings settings, ModalViewManager modalViewManager)
        {
            _service = service;
            _settings = settings;
            ConnectCommand = new DelegateCommand<object>(
                (o) =>
                {
                    _settings.Save();
                    _service.Connect();
                    if (_service.IsConnect)
                    {
                        modalViewManager.Hide();
                    }
                    HasProblems = !_service.IsConnect;
                });
        }

        public string ServerName
        {
            get { return _settings.ServerName; }
            set { _settings.ServerName = value;}
        }

        public string DBName
        {
            get { return _settings.DBName; }
            set { _settings.DBName = value; }
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
