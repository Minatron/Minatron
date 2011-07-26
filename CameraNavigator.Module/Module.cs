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
    public class CameraNavigatorModule : IModule, IActivatable
    {
        private readonly IUnityContainer _container;
        bool _activated = false;
       IRegionManager RegionManager { get; set; }
       public CameraNavigatorModule(IRegionManager regionManager, IUnityContainer container, ModulesActivator activator)
       {
           _container = container;
      
           activator.Register(this);
           RegionManager = regionManager;
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
       void InitializeAfterActivate()
       {
           _container.RegisterInstance(new Controller(null, "http://ns.e105.ru:3082", "http://ns.e105.ru:3084", "admin",
                                                     ""));
          // _container.RegisterInstance(new Controller(null, "http://62.113.49.111:8080", "http://62.113.49.111:3084", "admin",
           //                                         ""));
           

           RegionManager.RegisterViewWithRegion(ShellRegionNames.CameraController, typeof(View.ControllerView));
       }

       public void Initialize()
       {
           
       }
    }
}
