using NHibernate;
using Band.Storage.Core;

namespace Band.Storage
{
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
            return _factory.Function(() => GetSameExtension.SameObject<T>(GetNewSession(), obj));
        }
    }
}
