using NHibernate;
using NHibernate.Criterion;

namespace ImagesStoreSystem.DBProvider.Core
{
    public class ReceivePlanPackIdentityFilter : IReceivePlanTaskFilter
    {
        long _packIdentity;

        public ICriteria AddCriteria(ICriteria criteria)
        {

			return criteria.Add(Restrictions.Eq("PackIdentity", _packIdentity));
        }

        public ReceivePlanPackIdentityFilter(long packIdentity)
        {
            _packIdentity = packIdentity;
        }
    }
}
