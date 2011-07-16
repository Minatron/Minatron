using CommonObjects;
using DictionaryModule;
using DictionaryModule.Presenters;
using ImagesStoreSystem.DBProvider.Core;
using LocalizationLibrary;
using Microsoft.Practices.Composite.Events;
using StorageModule.Services;

namespace ReceivePlanModule.Presenters
{
	class StationFilterPresenter : FilterPresenter<IReceivePlanTaskFilter>
	{
		public DictionaryPresenter StationSelector { get; protected set; }

		public override string Name
		{
			get
			{
                return Lang.GetTitle("Modules/ReceivePlan/FiltersDescription/Station");
			}
		}

		public override IReceivePlanTaskFilter[] Filters
		{
			get
			{
				if (StationSelector.CatalogNumber.HasValue)
				{
					return new IReceivePlanTaskFilter[] { new StationFilter(StationSelector.CatalogNumber.Value) };
				}
				return null;
			}
		}

		public override void Reset()
		{
			StationSelector.CatalogNumber = null;
		}

		public StationFilterPresenter(StorageService storage, IEventAggregator eventAggregator, DictionaryLibrary dictLib)
			: base(eventAggregator)
		{
			StationSelector = dictLib.CreatePresenter(StorageModule.Model.DictionaryType.Station, true);
			StationSelector.OnSelectedChanged = () => FilterChanged();
		}
	}
}
