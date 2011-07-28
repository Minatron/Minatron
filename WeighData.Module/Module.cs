using Band.Client.Infrastructure;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace Band.Module.WeighData
{
    public class WeighDataModule : IModule, IActivatable
    {
        bool _activated;
        readonly IRegionManager _regionManager;
        readonly IUnityContainer _container;

        public WeighDataModule(IRegionManager regionManager, IUnityContainer container, ModulesActivator activator)
        {
            _container = container;
            _regionManager = regionManager;
            activator.Register(this);
        }
        
        public bool Activated
        {
            get { return _activated; }
            set
            {
                if (value && !_activated)
                {
                    _activated = true;
                    InitializeAfterActivate();
                }
            }
        }

        public void Initialize()
        {
            _container
                .RegisterType<Views.WeighDataView>(new ContainerControlledLifetimeManager())
                .RegisterType<Views.WeighDataCamerasView>(new ContainerControlledLifetimeManager());
        }

        void InitializeAfterActivate()
        {
            _regionManager.RegisterViewWithRegion(ShellRegionNames.Content, typeof(Views.WeighDataView));
        }
    }
}
