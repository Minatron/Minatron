using System;
using System.Diagnostics;
using NHibernate;
using NHibernate.Criterion;

namespace Band.Storage
{
    public class SearchFilterByIdentifyObject 
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

        public SearchFilterByIdentifyObject(Core.IdentifiableObject obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            _objectId = obj.Id;
            _propertyName = propertyName;
        }

        public SearchFilterByIdentifyObject(Core.IdentifiableObject obj) : this(obj, obj.GetType().Name) { }
    }
}
