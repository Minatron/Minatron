using System;
using Band.Storage;
using Band.Storage.Minatron;

namespace Band.OLD
{
    public class WeightTimeFilterPresenter : FilterPresenter
    {
        DateTime _startTime;
		DateTime _endTime;

		public DateTime StartTime
		{
			get
			{
				return _startTime;
			}
			set
			{
				_startTime = value;
				OnPropertyChanged("StartTime");
			}
		}
		public DateTime EndTime
		{
			get
			{
				return _endTime;
			}
			set
			{
				_endTime = value;
				OnPropertyChanged("EndTime");
			}
		}

		public override string Name
		{
			get
			{
                return @"Время";
			}
		}

		public override IStorageFilter[] Filters
		{
			get
			{
				return new IStorageFilter[] { new WeightTimeFilter(StartTime, EndTime) };
			}
		}

		public override void Reset()
		{
			StartTime = new DateTime(2011, 1, 1);
			EndTime = DateTime.UtcNow.AddDays(1).Date;
		}

        public WeightTimeFilterPresenter()
		{
			Reset();
		}
    }
}
