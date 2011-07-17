using NHibernate;

namespace Band.Storage.Core
{
    public interface ISessionCreator
    {
        ISession OpenSession();
    }
}
