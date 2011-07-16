using System;

namespace ImagesStoreSystem.DBProvider.Core
{
	public class ImageTimeFilter : PeriodFilter, IImageFilter
	{
		public ImageTimeFilter(DateTime? start = null, DateTime? end = null) :
			base(start, end)
		{
			m_startFieldName = "SurveyTime";
			m_endFieldName = "SurveyTime";
		}
	}
}
