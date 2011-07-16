using System.Configuration;

namespace WPF.Patterns.Settings
{
    

    public class AppSettings
    {               
        public static void Set(string name, string value)
        {            
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);         
            config.AppSettings.Settings.Add(name, value);           
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string Get(string name,string defaultValue=null)
        {
            var value =ConfigurationManager.AppSettings.Get(name);
            return (!string.IsNullOrWhiteSpace(value)) ? value : defaultValue;
        }
    }
}
