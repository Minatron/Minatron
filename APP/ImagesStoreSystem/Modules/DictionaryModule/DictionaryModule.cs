using DictionaryModule.Views;
using ImageStoreSystem.Infrastructure;
using LocalizationLibrary;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using StorageModule.Model;
using StorageModule.Services;

namespace DictionaryModule
{
	[ModuleDependency("SettingsModule")]
	public class DictionaryModule : IModuleWithCondition
	{
		IRegionManager RegionManager { get; set; }
		IUnityContainer Container { get; set; }

		DictionaryLibrary _library;

		public DictionaryModule(IUnityContainer container, IRegionManager regionManager)
		{
			RegionManager = regionManager;
			Container = container;

			container.RegisterType<DictionaryLibrary>(new ContainerControlledLifetimeManager());
		}


		public void Initialize()
		{	
			Container.Resolve<ModulesWithConditionDictionary>().AddModule(this, () => Container.Resolve<StorageService>().IsConnect);
		}

		public void ConditionHappened()
		{
			_library = Container.Resolve<DictionaryLibrary>();

			var menuAggregator = Container.Resolve<MainMenuAggregator>();
			var createMenuRegion = RegionManager.Regions[ShellRegionNames.CreateItemMenu];
			var editMenuRegion = RegionManager.Regions[ShellRegionNames.EditItemMenu];

			menuAggregator.RegisterMenuItem(createMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/CreateMenuHeaders/AttributeType")), isActive => { if (isActive) _library.ShowCreateView(DictionaryType.AdditionalField); }, false);
			menuAggregator.RegisterMenuItem(createMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/CreateMenuHeaders/Level")), isActive => { if (isActive) _library.ShowCreateView(DictionaryType.ImageLevel); }, false);
			menuAggregator.RegisterMenuItem(createMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/CreateMenuHeaders/Satellite")), isActive => { if (isActive) _library.ShowCreateView(DictionaryType.Satellite); }, false);
			menuAggregator.RegisterMenuItem(createMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/CreateMenuHeaders/SensorType")), isActive => { if (isActive) _library.ShowCreateView(DictionaryType.Sensor); }, false);
			menuAggregator.RegisterMenuItem(createMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/CreateMenuHeaders/Station")), isActive => { if (isActive) _library.ShowCreateView(DictionaryType.Station); }, false);
			menuAggregator.RegisterMenuItem(createMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/CreateMenuHeaders/Storage")), isActive => { if (isActive) _library.ShowCreateView(DictionaryType.BackupStorage); }, false);

			menuAggregator.RegisterMenuItem(editMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/EditionMenuHeaders/AttributeType")), isActive => { if (isActive) _library.ShowEditView(DictionaryType.AdditionalField); }, false);
			menuAggregator.RegisterMenuItem(editMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/EditionMenuHeaders/Level")), isActive => { if (isActive) _library.ShowEditView(DictionaryType.ImageLevel); }, false);
			menuAggregator.RegisterMenuItem(editMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/EditionMenuHeaders/Satellite")), isActive => { if (isActive) _library.ShowEditView(DictionaryType.Satellite); }, false);
			menuAggregator.RegisterMenuItem(editMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/EditionMenuHeaders/SensorType")), isActive => { if (isActive) _library.ShowEditView(DictionaryType.Sensor); }, false);
			menuAggregator.RegisterMenuItem(editMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/EditionMenuHeaders/Station")), isActive => { if (isActive) _library.ShowEditView(DictionaryType.Station); }, false);
			menuAggregator.RegisterMenuItem(editMenuRegion, new MenuHeaderView(new Title(@"Modules/Dictionary/EditionMenuHeaders/Storage")), isActive => { if (isActive) _library.ShowEditView(DictionaryType.BackupStorage); }, false);
		}
	}
}
