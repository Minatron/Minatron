using System;
using ImagesStoreSystem.DBProvider.Core;
using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Events;

namespace StorageModule.Services
{
    public class StorageService : ISessionCreator
    {
        readonly Properties.Settings _settings;
        readonly IEventAggregator _eventAgregator;
        readonly DataBaseProvider _db;

        public StorageService(Properties.Settings settings, IEventAggregator eventAgregator)
        {
            _settings = settings;
            _eventAgregator = eventAgregator;
			_db = new DataBaseProvider() { LoginInfo = new DataBase.LoginInfo() };

            //Connect();
        }

        internal void Connect()
        {
            _db.LoginInfo.SSPI = true;
            _db.LoginInfo.Server = _settings.ServerName;
            _db.LoginInfo.DBName = _settings.DBName;
            _db.Connect();
            _eventAgregator.GetEvent<RefreshAllEvent>().Publish(null);
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
