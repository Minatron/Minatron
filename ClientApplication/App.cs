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
            var resourses = new ResourceDictionary();
            resourses.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Pack://application:,,,/Themes/ExpressionLight.xaml", UriKind.Absolute) });
            resourses.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Pack://application:,,,/Themes/GroupBoxStyle.xaml", UriKind.Absolute) });
            Resources = resourses;		
        }
		               
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new BootStrapper().Run();
        }
    }
}
