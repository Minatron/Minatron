using System;
using Band.CameraNavigator.Module.Presenter;
using Band.CameraNavigator.Module.View;
using Band.Client.Infrastructure;
using CameraController;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace Band.CameraNavigator.Module
{
    public class CameraNavigatorModule : IModule
    {
        private readonly IUnityContainer _container;
        bool _activated = false;
       IRegionManager RegionManager { get; set; }
       public CameraNavigatorModule(IRegionManager regionManager, IUnityContainer container)
       {
           _container = container;
      
           RegionManager = regionManager;
       }

       public void Initialize()
       {
         //  _container.RegisterInstance(new Controller(null, "http://ns.e105.ru:3082", "http://ns.e105.ru:3084", "admin",
          //                                           ""));
           _container.RegisterInstance(new Controller(null, "http://62.113.49.111:8080", "http://62.113.49.111:3084", "admin",
                                                    ""));
           var presenter = _container.Resolve<ControllerPresenter>();
           _container.RegisterInstance(presenter);

           RegionManager.RegisterViewWithRegion(ShellRegionNames.CameraController, typeof(View.ControllerView));
       }

   
    }
}
