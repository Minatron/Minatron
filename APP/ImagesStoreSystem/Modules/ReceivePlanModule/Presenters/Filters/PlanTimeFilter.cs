using System;
using CommonObjects;
using ImagesStoreSystem.DBProvider.Core;
using LocalizationLibrary;
using Microsoft.Practices.Composite.Events;

namespace ReceivePlanModule.Presenters
{
	class PlanTimeFilter : FilterPresenter<IReceivePlanTaskFilter>
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
                return Lang.GetTitle("Modules/ReceivePlan/FiltersDescription/ReceiveTime");
			}
		}

		public override IReceivePlanTaskFilter[] Filters
		{
			get
			{
				return new IReceivePlanTaskFilter[] { new ReceivePlanTimeFilter(StartTime, EndTime) };
			}
		}

		public override void Reset()
		{
			StartTime = new DateTime(2000, 1, 1);
			EndTime = DateTime.UtcNow;
		}

		public PlanTimeFilter(IEventAggregator eventAggregator)
			: base(eventAggregator)
		{
			Reset();
		}
	}
}
