using NHibernate;

namespace Band.Storage.Core
{
    public static class GetSameExtension
    {
        public static T SameObject<T>(ISession session, T obj) where T : UpdatableObject
        {
            if (obj == null) return null;
            return session.Load<T>(obj.Id);
        }
    }
}
