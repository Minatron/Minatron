using NHibernate;

namespace ImagesStoreSystem.DBProvider.Core
{
	public interface IStorageFilter
	{
		ICriteria AddCriteria(ICriteria criteria);
	}
}
