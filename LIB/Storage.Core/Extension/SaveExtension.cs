using NHibernate;

namespace Band.Storage.Core
{
    public static class SaveExtension
    {


        public static T Save<T>(this T obj, ISession session) where T : UpdatableObject
        {
            return SaveBase<T>(obj, session);
        }

        private static T SaveBase<T>(T obj, ISession session)
        {
            try
            {
                session.SaveOrUpdate(obj);
            }
            catch (NHibernate.NonUniqueObjectException)
            {
                session.Flush();
                session.Clear();
                session.SaveOrUpdate(obj);
            }
            session.Flush();

            return obj;
        }
    }
}
