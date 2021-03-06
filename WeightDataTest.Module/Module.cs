﻿using Band.Client.Infrastructure;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using WeightDataTest.Module.Views;

namespace WeightDataTest.Module
{
    public class WeighDataTestModule : IModule, IActivatable
    {
        bool _activated = false;
        IRegionManager RegionManager { get; set; }

        public WeighDataTestModule(IRegionManager regionManager, IUnityContainer container, ModulesActivator activator)
        {
            RegionManager = regionManager;
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
        }

        void InitializeAfterActivate()
        {
            RegionManager.RegisterViewWithRegion(ShellRegionNames.Content, typeof(WeighDataView));
        }
    }
}
