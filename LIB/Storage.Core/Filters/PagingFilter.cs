using System.Diagnostics;
using NHibernate;

namespace Band.Storage
{
    public class PagingFilter : IStorageFilter
    {
        int m_copacity;
        int m_number;

        public ICriteria AddCriteria(ICriteria criteria)
        {
            Debug.Assert(m_copacity > 0);
            Debug.Assert(m_number >= 0);

            return criteria.SetMaxResults(m_copacity).SetFirstResult(m_copacity * m_number);
        }

        public PagingFilter(int copacity, int number = 0)
        {
            m_copacity = copacity;
            m_number = number;
        }
    }
}
