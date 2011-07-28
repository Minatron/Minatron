using Band.Storage.Core;
using Band.Storage.Minatron.Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Band.Storage.Minatron.Core
{
    public class DataBase : ISessionCreator
    {
       
        readonly ISessionFactory _sessionFactory;

        public DataBase(LoginInfo info)
        {

            try
            {
                var conf = Fluently.Configure()
                                    .Database(MsSql2008Conf(info)
                                    .Dialect<NHibernate.Dialect.MsSql2008Dialect>()
                                    .ProxyFactoryFactory("NHibernate.ByteCode.LinFu.ProxyFactoryFactory,NHibernate.ByteCode.LinFu"))
                                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<WeighData>()).BuildConfiguration();

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
