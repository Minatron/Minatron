using System.Diagnostics;
using NHibernate;
using NHibernate.Criterion;

namespace ImagesStoreSystem.DBProvider.Core
{
	public class ImageCloudnessFilter : IImageFilter
	{
		float m_maxCloudness;

		public ICriteria AddCriteria(ICriteria criteria)
		{
			Debug.Assert(m_maxCloudness >= 0 && m_maxCloudness <= 100);
			return criteria.Add(Restrictions.Le("Cloudiness", m_maxCloudness));
		}

		public ImageCloudnessFilter(float maxCloudness)
		{
			m_maxCloudness = maxCloudness;
		}
	}
}
