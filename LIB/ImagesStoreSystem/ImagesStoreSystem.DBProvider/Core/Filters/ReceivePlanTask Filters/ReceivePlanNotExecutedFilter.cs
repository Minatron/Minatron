using NHibernate;
using NHibernate.Criterion;

namespace ImagesStoreSystem.DBProvider.Core
{
    public class ReceivePlanNotExecutedFilter : IReceivePlanTaskFilter
    {
        public ICriteria AddCriteria(ICriteria criteria)
        {

            return criteria.Add(Restrictions.Eq("Aborted", false)).Add(Restrictions.IsNull("ResultSession"));
        }

    }
}
