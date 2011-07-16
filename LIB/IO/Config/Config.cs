using System;
namespace Lacross.IO
{
    public class Config
    {
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string FileName;
        public string FullFileName
        {
            get
            {
                string ModuleFullFileName = Lacross.OS.WinBase.GetModuleFileName(System.IntPtr.Zero);
                string StartupPath = System.IO.Path.GetDirectoryName(ModuleFullFileName);
                if (StartupPath == "") StartupPath = System.IO.Path.GetPathRoot(ModuleFullFileName);
                else StartupPath += System.IO.Path.DirectorySeparatorChar;
                return StartupPath + FileName; 
            }
        }

        public Config()
        {
            FileName = @"default.config";
        }
        public virtual void Set(object obj)
        {

        }
        public void Save()
        {
            System.Xml.Serialization.XmlSerializer myXmlSer = new System.Xml.Serialization.XmlSerializer(GetType());
            System.IO.StreamWriter myWriter = new System.IO.StreamWriter(FullFileName);
            myXmlSer.Serialize(myWriter, this);
            myWriter.Close();
        }
        public void Load(string fileName)
        {
            FileName = fileName;
            Load();
        }
        public void Load()
        {
            if (System.IO.File.Exists(FullFileName))
            {
                System.Xml.Serialization.XmlSerializer myXmlSer = new System.Xml.Serialization.XmlSerializer(GetType());
                System.IO.FileStream mySet = new System.IO.FileStream(FullFileName, System.IO.FileMode.Open);
				try
				{
					Set(myXmlSer.Deserialize(mySet));
				}
				catch (Exception ex)
				{
					mySet.Close();
					throw ex;
				}
				finally
				{
					mySet.Close();
				}
            }
        }

    }
}
