using CommonObjects;
using DictionaryModule;
using ImagesStoreSystem.DBProvider.Core;
using Microsoft.Practices.Composite.Events;
using StorageModule.Services;

namespace ReceivePlanModule.Presenters
{
	public class ReceivePlanFilterManager : FiltersManagerPresenter<IReceivePlanTaskFilter>
	{
		IEventAggregator _eventAggregator;

		public ReceivePlanFilterManager(StorageService storage, IEventAggregator eventAggregator, DictionaryLibrary dictLib)
			: base() 
		{
			_eventAggregator = eventAggregator;
			RegisteringFilters(
				new PlanTimeFilter(eventAggregator),
				new SatelliteFilterPresenter(storage, eventAggregator, dictLib),
				new StationFilterPresenter(storage, eventAggregator, dictLib),
				new NonExecutedFilterPresenter(eventAggregator),
				new PackIdentityFilterPresenter(eventAggregator)
				);
		}
	}
}
