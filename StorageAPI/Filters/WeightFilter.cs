using NHibernate;
using NHibernate.Criterion;

namespace Band.Storage.Minatron
{
    public class WeightFilter : IStorageFilter
    {
        float _minWeight;

		public ICriteria AddCriteria(ICriteria criteria)
		{
            return criteria.Add(Restrictions.Ge("Weigh", _minWeight));
		}

        public WeightFilter(float minWeight)
		{
            _minWeight = minWeight;
		}
    }
}
