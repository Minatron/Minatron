using System;
using System.Diagnostics;
using NHibernate;
using NHibernate.Criterion;

namespace ImagesStoreSystem.DBProvider.Core
{
	public class SearchFilterByIdentifyObject : IReceiveSessionFilter, IImageFilter
	{
		string _propertyName;

		long _objectId = 0;

		public ICriteria AddCriteria(ICriteria criteria)
		{
			Debug.Assert(_objectId >= 0);
			var rest = Restrictions.Eq("Id", _objectId);
			var subcriteria = criteria.CreateCriteria(_propertyName);
			subcriteria.Add(rest);
			return criteria;
		}

		public SearchFilterByIdentifyObject(IdentifiableObject obj, string propertyName)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			_objectId = obj.Id;
			_propertyName = propertyName;
		}

		public SearchFilterByIdentifyObject(IdentifiableObject obj) : this(obj, obj.GetType().Name) { }
	}
}
