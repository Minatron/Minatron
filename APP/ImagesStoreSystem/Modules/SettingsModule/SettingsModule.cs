using ImageStoreSystem.Infrastructure;
using LocalizationLibrary;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using SettingsModule.Views;

namespace SettingsModule
{
	[Module(ModuleName = "SettingsModule")]
    public class SettingsModule : IModule
    {
        public IRegionManager RegionManager { get; private set; }
        public IUnityContainer Container { get; private set; }
		public EventAggregator EventAggregator { get; protected set; }

        public SettingsModule(IRegionManager regionManager, IUnityContainer container, EventAggregator eventAgregator, Properties.Settings settings)
        {
            RegionManager = regionManager;
            Container = container;
			EventAggregator = eventAgregator;

			Lang.SetLang(settings.Lang);

			EventAggregator.GetEvent<ActivateSettingsEvent>().Subscribe(
				arg =>
				{
					EventAggregator.GetEvent<ActivateModalViewEvent>().Publish(Container.Resolve<SettingsContentView>());
				}, true);
        }


        public void Initialize()
        {          
            ConfigureContainer();
			var menuAggregator = Container.Resolve<MainMenuAggregator>();

			menuAggregator.RegisterMenuItem(RegionManager.Regions[ShellRegionNames.SettingsMenu], new SettingsMenuItem(),
				isActivate =>
				{
					if (isActivate)
					{
						EventAggregator.GetEvent<ActivateSettingsEvent>().Publish(null);
					}
				}, false);

			RegionManager.RegisterViewWithRegion(ShellRegionNames.Settings, typeof(Views.LangSettingView));
		
		}

        void ConfigureContainer()
        {
            Container
				.RegisterType<SettingsContentView>(new ContainerControlledLifetimeManager());
        }
    }
}
