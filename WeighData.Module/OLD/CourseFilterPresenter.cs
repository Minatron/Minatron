using Band.Storage;
using Band.Storage.Minatron;

namespace Band.OLD
{
    public class CourseFilterPresenter : FilterPresenter
    {
        CourseType _course = CourseType.Moscow;

		public CourseType Course 
		{ 
			get
			{
				return _course;
			}
			set
			{
                _course = value;
				OnPropertyChanged("CourseType");
			}
		}

		public override string Name
		{
			get
			{
                return @"Направление";
			}
		}

        public override IStorageFilter[] Filters
        {
            get
            {
                return new IStorageFilter[] { new CourseFilter(Course)};
            }
        }

		public override void Reset()
		{
			Course = CourseType.Moscow;
		}

        public CourseFilterPresenter()
		{
			Reset();
		}
    }
}
