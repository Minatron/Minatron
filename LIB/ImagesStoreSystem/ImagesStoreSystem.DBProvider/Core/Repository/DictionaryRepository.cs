using System.Collections.Generic;
using ImagesStoreSystem.DBProvider.Core.Extension;

namespace ImagesStoreSystem.DBProvider.Core
{
    public class DictionaryRepository<T> : UpdatableRepository<T> where T : DictionaryBase
    {
        public DictionaryRepository(ISessionCreator factory)
            : base(factory)
        { }

        public IList<T> GetAll()
        {
            return _factory.Function(() => Extension.GetAll.Dictonary<T>(GetNewSession()));
        }
    }
}
