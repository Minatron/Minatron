using System;
using System.Windows.Data;

namespace Band.WPF.Localization
{
    public class Title : Binding
    {
        public Title()
        {
            Source = Lang.Provider;
        }

        public Title(String path)
        {
            Source = Lang.Provider;
            XPath = path;
        }
    }
}
