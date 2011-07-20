using Band.Client.Infrastructure;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace Band.Module.WeighData
{
    public class Module : IModule
    {
        IRegionManager RegionManager { get; set; }
        public Module(IRegionManager regionManager, IUnityContainer container, IEventAggregator eventAggregator)
        {
            RegionManager = regionManager;
        }
        public void Initialize()
        {
            RegionManager.RegisterViewWithRegion(ShellRegionNames.Content, typeof(Views.WeighDataView));
        }
    }
}
