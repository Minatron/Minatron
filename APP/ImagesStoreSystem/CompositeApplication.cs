using System;
using System.Diagnostics;
using System.Windows;

namespace ImagesStoreSystem
{
    public class CompositeApplication : Application
    {

        CompositeApplication()
        {
            var resourses = new ResourceDictionary();
			resourses.MergedDictionaries.Add(new ResourceDictionary(){ Source = new Uri("Pack://application:,,,/ExpressionLight.xaml", UriKind.Absolute) });
			resourses.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Pack://application:,,,/GroupBoxStyle.xaml", UriKind.Absolute) });
			Resources = resourses;
        }

        [STAThreadAttribute]
        [DebuggerNonUserCodeAttribute]
        public static void Main()
        {
			new CompositeApplication().Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new Bootstrapper().Run();
        }
	}
}
