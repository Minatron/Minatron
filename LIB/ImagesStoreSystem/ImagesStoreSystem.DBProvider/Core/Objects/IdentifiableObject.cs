namespace ImagesStoreSystem.DBProvider.Core
{
    public abstract class IdentifiableObject
    {
        /// <summary>
        /// Идентификатор объекта
        /// NOT NULL
        /// </summary>
        public virtual long Id { get; set; }
    }
}
