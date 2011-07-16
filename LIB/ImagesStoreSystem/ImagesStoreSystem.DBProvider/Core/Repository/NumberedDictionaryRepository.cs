using ImagesStoreSystem.DBProvider.Core.Extension;

namespace ImagesStoreSystem.DBProvider.Core
{
	public class NumberedDictionaryRepository<T> : DictionaryRepository<T> where T : NumberedDictionaryBase
	{
		public NumberedDictionaryRepository(ISessionCreator factory)
            : base(factory)
        { }

		public T GetBy(long catalogNumber)
		{
			return _factory.Function(() => Extension.GetOne.DictionaryItem<T>(GetCurSession(), catalogNumber));
		}
	}
}
