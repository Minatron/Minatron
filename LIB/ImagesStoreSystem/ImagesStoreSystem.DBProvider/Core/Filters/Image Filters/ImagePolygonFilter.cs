using Microsoft.SqlServer.Types;
using NHibernate;
using NHibernate.Criterion;

namespace ImagesStoreSystem.DBProvider.Core
{
	public class ImagePolygonFilter : IImageFilter
	{
		SqlGeography m_polygon;

		public ICriteria AddCriteria(ICriteria criteria)
		{
			return criteria.Add(Expression.Sql(string.Format(@"geography::STGeomFromText('{0}', 4326).STIntersects([ImagePolygon]) = 1", new string(m_polygon.STAsText().Value))));
		}

		public ImagePolygonFilter(SqlGeography polygon)
		{
			m_polygon = polygon;
		}
	}
}
