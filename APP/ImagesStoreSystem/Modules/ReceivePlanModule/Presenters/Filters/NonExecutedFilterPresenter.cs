using System;
using CommonObjects;
using ImagesStoreSystem.DBProvider.Core;
using LocalizationLibrary;
using Microsoft.Practices.Composite.Events;

namespace ReceivePlanModule.Presenters
{
	class NonExecutedFilterPresenter : FilterPresenter<IReceivePlanTaskFilter>
	{
		DateTime _fromDateTime;

		public DateTime FromDateTime
		{
			get
			{
				return _fromDateTime;
			}
			protected set
			{
				_fromDateTime = value;
				OnPropertyChanged("FromDateTime");
				OnPropertyChanged("Name");
			}
		}

		public override string Name
		{
			get
			{
                return Lang.GetTitle("Modules/ReceivePlan/FiltersDescription/OnlyPlanned");
			}
		}

		public override IReceivePlanTaskFilter[] Filters
		{
			get 
			{
				return new IReceivePlanTaskFilter[] { 
									new ReceivePlanNotExecutedFilter(),
									new ReceivePlanTimeFilter(_fromDateTime)};
			}
		}

		public override void Reset()
		{
			FromDateTime = DateTime.UtcNow - ReceivePlanConstants.Delta;
		}

		public NonExecutedFilterPresenter(IEventAggregator eventAggregator)
			: base(eventAggregator) 
		{
			Reset();
		}
	}
}
