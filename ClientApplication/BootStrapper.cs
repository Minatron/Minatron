using System.Windows;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Unity;

namespace Band.Client.App
{
    class BootStrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var app = Application.Current;
            app.MainWindow = Container.Resolve<Shell>();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.MainWindow.Show();
            return app.MainWindow;
        }

        protected override IModuleCatalog GetModuleCatalog()
        {
            return new ModuleCatalog();
            //return new DirectoryModuleCatalog { ModulePath = @".\Modules" };
        }
    }
}
