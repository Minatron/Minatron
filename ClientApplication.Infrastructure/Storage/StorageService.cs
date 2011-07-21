using Band.Storage.Core;
using Band.Storage.Minatron.Core;
using Microsoft.Practices.Composite.Events;

namespace Band.Client.Infrastructure.Storage
{
    public class StorageService : ISessionCreator
    {
        readonly Properties.AppSettings _settings;
        readonly IEventAggregator _eventAgregator;
        readonly DataBaseProvider _db;
        readonly ModulesActivator _activator;

        public StorageService(Properties.AppSettings settings, IEventAggregator eventAgregator , ModulesActivator activator)
        {
            _settings = settings;
            _eventAgregator = eventAgregator;
            _db = new DataBaseProvider() { LoginInfo = new LoginInfo() };
            _activator = activator;
        }

        public void Connect()
        {
            _db.LoginInfo.SSPI = true;
            _db.LoginInfo.Server = _settings.ServerName;
            _db.LoginInfo.DBName = _settings.DBName;
            _db.Connect();
            
            _activator.Activated = IsConnect;
        }

        public bool IsConnect
        {
            get
            {
                return _db.IsConnect;
            }
        }

        public NHibernate.ISession OpenSession()
        {
            return _db.OpenSession();
        }
    }
}
