using NHibernate;
using Band.Storage.Core;

namespace Band.Storage
{
    public class RepositoryBase<T> where T : RemovableObject
    {
        protected ISessionCreator _factory;
        ISession _session;

        public RepositoryBase(ISessionCreator factory)
        {
            _factory = factory;
        }

        protected ISession GetCurSession()
        {
            if (_session == null) _session = _factory.OpenSession();
            return _session;
        }

        protected ISession GetNewSession()
        {
            CloseSession();
            return GetCurSession();
        }

        public void CloseSession()
        {
            if (_session != null)
            {
                _session.Close();
                _session = null;
            }
        }

        public void Remove(T obj)
        {
            _factory.TransactionAction(s => obj.RemoveFrom<T>(s));
        }
    }
}
