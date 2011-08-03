using System;
using NHibernate;
using NHibernate.Criterion;

namespace Band.Storage.Filters
{
    public abstract class PeriodFilter : IStorageFilter
    {
        DateTime? _startTime;
        DateTime? _endTime;

        protected String _startFieldName;
        protected String _endFieldName;

        public ICriteria AddCriteria(ICriteria criteria)
        {
            ICriteria res = criteria;
            if (_startTime.HasValue)
            {
                res = res.Add(Restrictions.Ge(_endFieldName, _startTime.Value));
            }
            if (_endTime.HasValue)
            {
                res = res.Add(Restrictions.Le(_startFieldName, _endTime.Value));
            }
            return res;
        }

        public PeriodFilter(DateTime? start = null, DateTime? end = null)
        {
            _startTime = start;
            _endTime = end;
        }
    }
}
