using Band.Storage.Core;

namespace Band.Storage.Minatron
{
    public static class RepositoryExtension
    {
        public static WeighDataRepository CreateImageRepository(this ISessionCreator factory)
        {
            return new WeighDataRepository(factory);
        }
    }
}
