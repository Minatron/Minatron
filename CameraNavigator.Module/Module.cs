using Band.Client.Infrastructure;
using CameraController;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace Band.CameraNavigator.Module
{
    public class CameraNavigatorModule : IModule
    {
        readonly IUnityContainer _container;
        readonly IRegionManager _regionManager;

        public CameraNavigatorModule(IRegionManager regionManager, IUnityContainer container)
        {
            _container = container;
            _regionManager = regionManager;
        }


        public void Initialize()
        {
            _container.RegisterInstance(new Controller(null, "http://ns.e105.ru:3082", "http://ns.e105.ru:3084", "admin",
                                                      ""));
            // _container.RegisterInstance(new Controller(null, "http://62.113.49.111:8080", "http://62.113.49.111:3084", "admin",
            //                                         ""));


            _regionManager.RegisterViewWithRegion(ShellRegionNames.CameraController, typeof(View.ControllerView));
        }


    }
}