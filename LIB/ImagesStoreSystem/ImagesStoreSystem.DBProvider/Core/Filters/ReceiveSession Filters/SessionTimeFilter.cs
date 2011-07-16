using System;
using NHibernate;
using NHibernate.Criterion;

namespace ImagesStoreSystem.DBProvider.Core
{
	public class SessionTimeFilter : PeriodFilter, IReceiveSessionFilter
	{

		public SessionTimeFilter(DateTime? start = null, DateTime? end = null)
			: base(start, end)
		{
			m_startFieldName = "StartTime";
			m_endFieldName = "EndTime";
		}
	}
}
