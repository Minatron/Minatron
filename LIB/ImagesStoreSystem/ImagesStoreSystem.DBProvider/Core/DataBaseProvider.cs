using NHibernate;

namespace ImagesStoreSystem.DBProvider.Core
{
    public class DataBaseProvider : ISessionCreator
    {
        DataBase _db;

        public DataBase.LoginInfo LoginInfo { get; set; }


        public bool IsConnect
        {
            get
            {
                if (_db == null) return false;
                return _db.IsConnect;
            }
        }

        public bool Connect()
        {
            Disconnect();
            if (LoginInfo != null) _db = new DataBase(LoginInfo);
            return IsConnect;
        }

        public void Disconnect()
        {
            if (_db != null)
            {
                _db.Close();
                _db = null;
            }
        }

        public ISession OpenSession()
        {
			if (_db == null) throw new ConnectException(@"You must call 'DataBaseProvider.Connect();' before this operation");
			try
			{				
				return _db.OpenSession();

			}
			catch(System.Exception ex)
			{
				throw ex;
			}
        }
    }
}
