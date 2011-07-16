using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using StorageModule.Services;
using StorageModule.Views;

namespace StorageModule
{
	[ModuleDependency("SettingsModule")]
    public class Module : IModule
    {
        IRegionManager RegionManager { get; set; }
        IUnityContainer Container { get; set; }
		IEventAggregator EventAggregator { get; set; }

        public Module(IRegionManager regionManager, IUnityContainer container, IEventAggregator eventAggregator)
        {
            RegionManager = regionManager;
            Container = container;
			EventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            RegisteringTypes();

			EventAggregator.GetEvent<ActivateModalViewEvent>().Publish(Container.Resolve<ConnectionView>());
        }

        void RegisteringTypes()
        {
			Container
				.RegisterType<Properties.Settings>(new ContainerControlledLifetimeManager())
				.RegisterType<StorageService>(new ContainerControlledLifetimeManager())
				.RegisterType<LostConnectView>(new ContainerControlledLifetimeManager())
				.RegisterType<ConnectionView>(new ContainerControlledLifetimeManager())
				.RegisterType<ContentTransferServiceAsync>(new ContainerControlledLifetimeManager())
				.RegisterType<ContentTransferService>(new ContainerControlledLifetimeManager(), new InjectionFactory(container => container.Resolve<ContentTransferServiceAsync>()));
        }
    }
}
