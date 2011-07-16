using ImagesStoreSystem.DBProvider.Core.Extension;
using NHibernate;

namespace ImagesStoreSystem.DBProvider.Core
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

    public class UpdatableRepository<T> : RepositoryBase<T> where T : UpdatableObject
    {
        public UpdatableRepository(ISessionCreator factory)
            : base(factory)
        { }

        public T Save(T obj)
        {
            return _factory.TransactionFunction(s => obj.Save<T>(s));
        }

		public void SaveAllChanges()
		{
            _factory.Function<object>(
                () =>
                {
                    GetCurSession().Flush();
                    return null;
                });

		}

        public T GetSameObject(T obj)
        {
            return _factory.Function(() => Extension.GetOne.SameObject<T>(GetNewSession(),obj));
        }
    }

}		
