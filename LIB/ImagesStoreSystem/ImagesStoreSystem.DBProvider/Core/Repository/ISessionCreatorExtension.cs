
namespace ImagesStoreSystem.DBProvider.Core
{
    public static class ISessionCreatorExtension
    {
        public static ImageRepository CreateImageRepository(this ISessionCreator factory)
        {
            return new ImageRepository(factory);
        }

        public static ReceiveSessionRepository CreateReceiveSessionRepository(this ISessionCreator factory)
        {
            return new ReceiveSessionRepository(factory);
        }

        public static ReceivePlanTaskRepository CreateReceivePlanTaskRepository(this ISessionCreator factory)
        {
            return new ReceivePlanTaskRepository(factory);
        }

        public static DataFileRepository CreateDataFileRepository(this ISessionCreator factory)
        {
            return new DataFileRepository(factory);
        }

        public static RepositoryBase<BackupFile> CreateBackupFileRepository(this ISessionCreator factory)
        {
            return new RepositoryBase<BackupFile>(factory);
        }

        public static UpdatableRepository<ImageAttribute> CreateImageAttributeRepository(this ISessionCreator factory)
        {
            return new UpdatableRepository<ImageAttribute>(factory);
        }

        public static DictionaryRepository<AttributeTitle> CreateAttributeTitleRepository(this ISessionCreator factory)
        {
            return new DictionaryRepository<AttributeTitle>(factory);
        }

		public static DictionaryRepository<T> CreateDictionaryRepository<T>(this ISessionCreator factory) where T : DictionaryBase
		{
			return new DictionaryRepository<T>(factory);
		}

		public static NumberedDictionaryRepository<T> CreateNumberedDictionaryRepository<T>(this ISessionCreator factory) where T : NumberedDictionaryBase
		{
			return new NumberedDictionaryRepository<T>(factory);
		}

		public static NumberedDictionaryRepository<Station> CreateStationRepository(this ISessionCreator factory)
        {
			return new NumberedDictionaryRepository<Station>(factory);
        }

		public static NumberedDictionaryRepository<Satellite> CreateSatelliteRepository(this ISessionCreator factory)
        {
			return new NumberedDictionaryRepository<Satellite>(factory);
        }

        public static DictionaryRepository<ImageLevel> CreateImageLevelRepository(this ISessionCreator factory)
        {
            return new DictionaryRepository<ImageLevel>(factory);
        }

        public static DictionaryRepository<SatelliteSensor> CreateSatelliteSensorRepository(this ISessionCreator factory)
        {
            return new DictionaryRepository<SatelliteSensor>(factory);
        }

        public static DictionaryRepository<BackupsStorage> CreateBackupsStorageRepository(this ISessionCreator factory)
        {
            return new DictionaryRepository<BackupsStorage>(factory);
        }
    }
}
