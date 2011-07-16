using System.IO;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.Infrastructure
{
    public class test
    {
        public test()
        {
            var parser = new RecivePlanFile();

            var list = parser.Parse(File.OpenRead(@"RecivePlanTest.xml"));

            var doc = parser.Write(list);

            File.WriteAllText(@"resultRecivePlanTest.xml", doc);
        }
    }
}
