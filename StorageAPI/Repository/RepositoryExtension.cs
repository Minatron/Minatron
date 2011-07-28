using Band.Storage.Core;

namespace Band.Storage.Minatron
{
    public static class RepositoryExtension
    {
        public static WeighDataRepository CreateWeighDataRepository(this ISessionCreator factory)
        {
            return new WeighDataRepository(factory);
        }
    }
}
