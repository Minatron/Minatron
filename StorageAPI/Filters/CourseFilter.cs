using NHibernate;
using NHibernate.Criterion;

namespace Band.Storage.Minatron
{
    public class CourseFilter : IStorageFilter
    {
        CourseType _course;

		public ICriteria AddCriteria(ICriteria criteria)
		{
            return criteria.Add(Restrictions.Eq("Course", _course));
		}

        public CourseFilter(CourseType course)
		{
            _course = course;
		}
    }
}
