using System.Windows;
using Band.Client.Infrastructure.Properties;
using Band.Client.Infrastructure.Storage;
using Band.WPF.Localization;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Unity;
using Band.Client.Infrastructure;

namespace Band.Client.App
{
    class BootStrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var app = Application.Current;
            app.MainWindow = Container.Resolve<Shell>();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;

            var settings = Container.Resolve<AppSettings>();
            Lang.SetLang(settings.Lang);

            app.MainWindow.Show();
            return app.MainWindow;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container
                .RegisterType<AppSettings>(new ContainerControlledLifetimeManager())
                .RegisterType<ModalViewManager>(new ContainerControlledLifetimeManager())
                .RegisterType<StorageService>(new ContainerControlledLifetimeManager());


        }
 

        protected override IModuleCatalog GetModuleCatalog()
        {
            return new ModuleCatalog();
            //return new DirectoryModuleCatalog { ModulePath = @".\Modules" };
        }
    }
}
