using System.Windows;
using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Unity;

namespace ImagesStoreSystem
{
	class Bootstrapper : UnityBootstrapper
    {              
        protected override DependencyObject CreateShell()
        {
            var app = CompositeApplication.Current;
            app.MainWindow = Container.Resolve<Shell>();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.MainWindow.Show();
            return app.MainWindow;
        }

		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();
			Container.RegisterType<ModulesWithConditionDictionary>(new ContainerControlledLifetimeManager());
			Container.RegisterType<MainMenuAggregator>(new ContainerControlledLifetimeManager());
			Container.RegisterType<ModalViewManager>(new ContainerControlledLifetimeManager());
		}
         
        protected override IModuleCatalog GetModuleCatalog()
        {
            return new DirectoryModuleCatalog { ModulePath = @".\Modules" };
        }
	}
}
