using Band.CameraNavigator.Module.Presenter;
using Band.Client.Infrastructure;
using Band.Client.Infrastructure.Properties;
using CameraController;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace Band.CameraNavigator.Module
{
    public class CameraNavigatorModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly AppSettings _settings;
        bool _activated = false;
       IRegionManager RegionManager { get; set; }
       public CameraNavigatorModule(IRegionManager regionManager, IUnityContainer container, AppSettings settings)
       {
           _container = container;
           _settings = settings;
           RegionManager = regionManager;
       }

       public void Initialize()
       {

           var conf = new CamerasConfigurator(_settings.CamerasConfigurator);
           _container.RegisterInstance(new Controller(conf, _settings.TServer, _settings.TVideoServer, _settings.TLogin, _settings.TPass
                                                    ));
           //_container.RegisterInstance(new Controller(null, "http://62.113.49.111:8080", "http://62.113.49.111:3084", "admin",
           //                                         ""));
           var presenter = _container.Resolve<ControllerPresenter>();
           _container.RegisterInstance(presenter);

           RegionManager.RegisterViewWithRegion(ShellRegionNames.CameraController, typeof(View.ControllerView));
       }

   
    }
}
