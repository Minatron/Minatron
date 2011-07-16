
namespace ImagesStoreSystem.DBProvider.Core
{
    public abstract class DictionaryBase : UpdatableObject
    {
        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual string Title { get; set; }
    }

	public abstract class NumberedDictionaryBase : DictionaryBase
	{
		/// <summary>
		/// Номер в каталоге
		/// NOT NULL
		/// </summary>
		public virtual long CatalogNumber { get; set; }
	}
}
