using System;
using System.Diagnostics;
using System.Windows;

namespace Band.Client.App
{
    public class App : Application
    {
        [STAThreadAttribute]
        [DebuggerNonUserCodeAttribute]
        public static void Main()
        {
          //  new SplashScreen("Splash.png").Show(true, true);
            new App().Run();
        }

        App()
        {
            Resources = new ResourceDictionary { Source = new Uri("Pack://application:,,,/Themes/ExpressionLight.xaml", UriKind.Absolute) };			
        }
		               
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new BootStrapper().Run();
        }
    }
}
