using System;
using Band.Storage.Filters;

namespace Band.Storage.Minatron
{
    public class WeightTimeFilter : PeriodFilter
    {
        public WeightTimeFilter(DateTime? start = null, DateTime? end = null) :
			base(start, end)
		{
            _startFieldName = "WeighTime";
            _endFieldName = "WeighTime";
		}
    }
}
