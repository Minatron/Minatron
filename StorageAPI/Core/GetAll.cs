using System.Collections.Generic;
using NHibernate;


namespace Band.Storage.Minatron.Core
{
    public static class GetAll
    {
        public static IList<WeighData> WeighData(ISession session, IEnumerable<IStorageFilter> filters)
        {
            ICriteria criteria = session.CreateCriteria<WeighData>();
            foreach (IStorageFilter filter in filters)
            {
                criteria = filter.AddCriteria(criteria);
            }
            return criteria.List<WeighData>();
        }
    }
}
