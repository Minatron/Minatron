using System;
using NHibernate;
using NHibernate.Criterion;

namespace ImagesStoreSystem.DBProvider.Core
{
	public class ImageSurveyTimeFilter : PeriodFilter, IImageFilter
	{
		public ImageSurveyTimeFilter(DateTime? start = null, DateTime? end = null)
			: base(start, end)
		{
			m_startFieldName = "SurveyTime";
			m_endFieldName = "SurveyTime";
		}
	}
}
