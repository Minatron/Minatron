using System;
using NHibernate;
using NHibernate.Criterion;

namespace ImagesStoreSystem.DBProvider.Core
{
	public abstract class PeriodFilter : IStorageFilter
	{
		DateTime m_startTime;
		DateTime m_endTime;

		protected String m_startFieldName;
		protected String m_endFieldName;

		public ICriteria AddCriteria(ICriteria criteria)
		{
			ICriteria res = criteria;
			if (m_startTime != DateTime.MinValue)
			{
				res = res.Add(Restrictions.Ge(m_endFieldName, m_startTime));
			}
			if (m_endTime != DateTime.MaxValue)
			{
				res = res.Add(Restrictions.Le(m_startFieldName, m_endTime));
			}
			return res;
		}

		public PeriodFilter(DateTime? start = null, DateTime? end = null)
		{
			m_startTime = start ?? DateTime.MinValue;
			m_endTime = end ?? DateTime.MaxValue;
		}
	}
}
