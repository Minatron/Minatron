using NHibernate;

namespace Band.Storage.Core
{
    public static class RemoveExtension
    {
        public static void RemoveFrom<T>(this T obj, ISession session) where T : RemovableObject
        {
            var tmp = session.Get<T>(obj.Id);      
            session.Delete(tmp);
            session.Flush();
        }
    }
}
