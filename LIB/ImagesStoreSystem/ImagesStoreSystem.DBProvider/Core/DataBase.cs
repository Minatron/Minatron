using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace ImagesStoreSystem.DBProvider.Core
{
    /// <summary>
    /// Провайдер к объектам из DB
    /// </summary>
    public class DataBase : ISessionCreator
    {
        public class LoginInfo
        {
            public string Server = @"winserver";
            public string DBName = @"ImagesStoreSystem";
            public string UserID = @"sa";
            public string PWD = @"barl123BARL";
            public bool SSPI = false;
            public string ConnectionString
            {
                get
                {
                    return string.Format("server={0}; database={1}; {2}", Server, DBName, (SSPI) ? @"Integrated Security=SSPI; MultipleActiveResultSets=True;" : string.Format(@"User ID={0}; Password={1}; Trusted_Connection=False; ", UserID, PWD));
                }
            }
        }


        readonly ISessionFactory _sessionFactory;
        public DataBase()
            : this(new LoginInfo())
        { }
        public DataBase(LoginInfo info)
        {

            try
            {
                var conf = Fluently.Configure()
                                    .Database(MsSql2008Conf(info)
                                    .Dialect<NHibernate.Dialect.MsSql2008Dialect>()
                                    .ProxyFactoryFactory("NHibernate.ByteCode.LinFu.ProxyFactoryFactory,NHibernate.ByteCode.LinFu"))
                                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Station>()).BuildConfiguration();

                _sessionFactory = conf.BuildSessionFactory();
            }
            catch 
            {
                IsConnect = false;
                return;
            }
            IsConnect = true;
            return;
        }

        private static MsSqlConfiguration MsSql2008Conf(LoginInfo info)
        {
            return MsSqlConfiguration.MsSql2008
                    .ConnectionString(info.ConnectionString);
        }

        public bool IsConnect { get; private set; }

        public void Close()
        {
            if (IsConnect)
            {
                IsConnect = false;
                _sessionFactory.Close();
            }
        }

        public ISession OpenSession()
        {
			if (!IsConnect) throw new ConnectException(@"No connection to DB");
			return _sessionFactory.OpenSession();
        }
    }
}
