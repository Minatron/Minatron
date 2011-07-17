using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Reflection;

namespace Band.LocalizationLibrary
{
	public class Lang
	{
		public const string RU = @"RU";
		public const string EN = @"EN";
        public static string TemplateFileName = @"{0}.xml";
		
		public static XmlDataProvider Provider {get; private set;}
		static Lang()
		{
			Provider = new XmlDataProvider {XPath = "LanguagesResource", IsAsynchronous = false};
		    IsRegistrated = false;
		}
		public static ComponentResourceKey res
		{
			get
			{
				return new ComponentResourceKey(typeof(Lang), "res");
			}
		}

        public static string[] GetLangs(string path = null)
		{
            if (path == null)
            {
                path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Languages";
            }
            if (!path.EndsWith(@"\"))
            {
                path += Path.DirectorySeparatorChar;
            }
            var list = new string[0];
            if (Directory.Exists(path))
            {
                list = Directory.GetFileSystemEntries(path, "*.xml");
                for (int i = 0; i < list.Length; i++) list[i] = Path.GetFileNameWithoutExtension(list[i]);
            }
			return list;
		}

		public static bool IsRegistrated { get; private set; }
		public void Registration()
		{
			if (IsRegistrated) return;

			Application.Current.Resources.Add(res, Provider);
			IsRegistrated = true;
		}

	    public static void SetFile(string filename)
		{			
			try
			{
				Provider.Source = new Uri(filename);
				var langString = GetTitle(@"@culture", System.Threading.Thread.CurrentThread.CurrentCulture.IetfLanguageTag);

				Application.Current.MainWindow.Language = System.Windows.Markup.XmlLanguage.GetLanguage(langString);
			}
			catch (UriFormatException)
			{
				Trace.WriteLine("Не удалось создать URL");
			}
		}
		
		public static void SetFileFromApplicationFolder(string filename)
		{
            SetFile(string.Format("pack://siteoforigin:,,,/{0}", filename));
		}
		public static void SetFileFromLangFolder(string filename)
		{
            SetFileFromApplicationFolder(string.Format("Languages/{0}", filename));
		}
		public static void SetLang(string lang)
		{
			SetFileFromLangFolder(string.Format(TemplateFileName, lang));
		}
			
		public static string GetTitle(string xpath)
		{
            return GetTitle(xpath, xpath);
		}
		public static string GetTitle(string xpath, string defaultTitle)
		{
			try
			{
				return Provider.Document.SelectSingleNode(Provider.XPath).SelectSingleNode(xpath).InnerText;	
			}
			catch(NullReferenceException)
			{
				return defaultTitle;
			}
		}

		public static string GetEnum(string xpath, object value)
		{
            return GetEnum(xpath, value, value.ToString());
		}

		public static string GetEnum(string xpath, object value, string defaultTitle)
		{
			return GetTitle(xpath + value, defaultTitle);
		}
	}
}
