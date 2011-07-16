using CommonObjects;
using DictionaryModule;
using DictionaryModule.Presenters;
using ImagesStoreSystem.DBProvider.Core;
using LocalizationLibrary;
using Microsoft.Practices.Composite.Events;
using StorageModule.Services;

namespace ReceivePlanModule.Presenters
{
	class SatelliteFilterPresenter : FilterPresenter<IReceivePlanTaskFilter>
	{
		public DictionaryPresenter SatelliteSelector { get; protected set; }

		public override string Name
		{
			get
			{
                return Lang.GetTitle("Modules/ReceivePlan/FiltersDescription/Satellite");
			}
		}

		public override IReceivePlanTaskFilter[] Filters
		{
			get
			{
				if (SatelliteSelector.CatalogNumber.HasValue)
				{
					return new IReceivePlanTaskFilter[] { new SatelliteFilter(SatelliteSelector.CatalogNumber.Value) };
				}
				return null;
			}
		}

		public override void Reset()
		{
			SatelliteSelector.CatalogNumber = null;
		}

		public SatelliteFilterPresenter(StorageService storage, IEventAggregator eventAggregator, DictionaryLibrary dictLib)
			: base(eventAggregator)
		{
			SatelliteSelector = dictLib.CreatePresenter(StorageModule.Model.DictionaryType.Satellite, true);
			SatelliteSelector.OnSelectedChanged = () => FilterChanged();
		}
	}
}
