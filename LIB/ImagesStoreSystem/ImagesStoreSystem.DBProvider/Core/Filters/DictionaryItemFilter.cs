using NHibernate;
using NHibernate.Criterion;

namespace ImagesStoreSystem.DBProvider.Core
{
	public class DictionaryItemFilter<T> : IStorageFilter where T : NumberedDictionaryBase
	{
		readonly string _fieldName;
		long _number;

		public ICriteria AddCriteria(ICriteria criteria)
		{
			return criteria.Add(Restrictions.Eq(_fieldName, _number));
		}

		public DictionaryItemFilter(long number, string fieldName)
		{
			_number = number;
			_fieldName = fieldName;
		}

		public DictionaryItemFilter(T obj, string fieldName)
			: this(obj.CatalogNumber, fieldName){}
	}

	public class StationFilter : DictionaryItemFilter<Station>, IReceiveSessionFilter, IReceivePlanTaskFilter
	{
		public StationFilter(long number)
			: base(number, @"StationCatalogNumber") { }

		public StationFilter(Station obj) : base(obj, @"StationCatalogNumber") { }
	}

	public class SatelliteFilter : DictionaryItemFilter<Satellite>, IReceiveSessionFilter, IReceivePlanTaskFilter, IImageFilter
	{
		public SatelliteFilter(long number)
			: base(number, @"SatelliteCatalogNumber") { }

		public SatelliteFilter(Satellite obj) : base(obj, @"SatelliteCatalogNumber") { }
	}
}
