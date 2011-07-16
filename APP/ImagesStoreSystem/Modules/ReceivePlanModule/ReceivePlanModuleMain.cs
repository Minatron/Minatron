using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using ReceivePlanModule.Presenters;
using ReceivePlanModule.View;
using StorageModule.Services;
using DictionaryModule;

namespace ReceivePlanModule
{
	[ModuleDependency("SettingsModule")]
	public class ReceivePlanModuleMain : IModuleWithCondition
	{
		IRegionManager RegionManager { get; set; }
		IUnityContainer Container { get; set; }
		IEventAggregator EventAggregator { get; set; }

		public ReceivePlanModuleMain(IRegionManager regionManager, IUnityContainer container, IEventAggregator eventAggregator)
		{
			RegionManager = regionManager;
			Container = container;
			EventAggregator = eventAggregator;
			RegisteringTypes();
		}

		public void Initialize()
		{
			Container.Resolve<ModulesWithConditionDictionary>().AddModule(this, () => Container.Resolve<StorageService>().IsConnect);
		}

		public void ConditionHappened()
		{
			RegionManager.RegisterViewWithRegion(ShellRegionNames.Content, Initialize_Menu_and_Content);
			Repositories.Init(Container.Resolve<StorageService>(), EventAggregator);
		}

		object Initialize_Menu_and_Content()
		{
			var menuAggregator = Container.Resolve<MainMenuAggregator>();
			var content = Container.Resolve<ReceivePlanPageView>();

			var menuItem = menuAggregator.RegisterMenuItem(RegionManager.Regions[ShellRegionNames.ReceiveSessionMenu], new ReceivePlanMenuItem(),
				isActivate =>
				{
					if (isActivate)
					{
						EventAggregator.GetEvent<ActivateEvent>().Publish(new ActivateViewEventArgs(RegionManager.Regions[ShellRegionNames.Content], content, isNewSequence:true));
					}
				});

			return content;
		}

		void RegisteringTypes()
		{
			Container.RegisterType<ReceivePlanPageView>(new InjectionFactory(c => new ReceivePlanPageView(CreateReceivePlanPage(c))));
		}

		ReceivePlanPagePresenter CreateReceivePlanPage(IUnityContainer container)
		{
			StorageService storage = container.Resolve<StorageService>();
			IEventAggregator eventAggregator = container.Resolve<IEventAggregator>();
			IRegionManager regionManager = container.Resolve<IRegionManager>();

			var filterManager = new ReceivePlanFilterManager(storage, eventAggregator, container.Resolve<DictionaryLibrary>());
			return new ReceivePlanPagePresenter(storage, eventAggregator, filterManager);
		}
	}
}
