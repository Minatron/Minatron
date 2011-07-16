using System;

namespace ImagesStoreSystem.DBProvider.Core
{
    public class ReceivePlanTimeFilter : PeriodFilter, IReceivePlanTaskFilter
    {

        public ReceivePlanTimeFilter(DateTime? start = null, DateTime? end = null)
            : base(start, end)
        {
            m_startFieldName = "StartTime";
            m_endFieldName = "EndTime";
        }
    }
}
