using NHibernate;

namespace Band.Storage
{
    public interface IStorageFilter
    {
        ICriteria AddCriteria(ICriteria criteria);
    }
}
