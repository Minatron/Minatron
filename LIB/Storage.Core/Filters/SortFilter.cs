using System;
using NHibernate;

namespace Band.Storage
{
    public class SortFilter : IStorageFilter
    {
        public enum Orders { ASC, DESC };

        String m_fieldName;
        Orders m_order;

        public ICriteria AddCriteria(ICriteria criteria)
        {
            if (m_order == SortFilter.Orders.ASC)
            {
                return criteria.AddOrder(NHibernate.Criterion.Order.Asc(m_fieldName));
            }
            else
            {
                return criteria.AddOrder(NHibernate.Criterion.Order.Desc(m_fieldName));
            }
        }

        public SortFilter(string fieldForSort, Orders sortType = Orders.ASC)
        {
            m_fieldName = fieldForSort;
            m_order = sortType;
        }
    }
}
