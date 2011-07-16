using NHibernate;
namespace ImagesStoreSystem.DBProvider.Core
{
    public interface ISessionCreator
    {
        ISession OpenSession();
    }
}


