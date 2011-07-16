using System;
using System.Data.SqlClient;
using NHibernate;

namespace ImagesStoreSystem.DBProvider.Core.Extension
{
    public static class SessionFactoryExtension
    {
        public static void TransactionAction(this ISessionCreator factory, Action<ISession> action)
        {
            TransactionFunction<object>(factory,(s) => { action(s); return null; });
        }

        public static T TransactionFunction<T>(this ISessionCreator factory, Func<ISession, T> action)
        {
            return ExceptionFunction(
                () =>
                {
                    T res;
                    using (var session = factory.OpenSession())
                    using (var tx = session.BeginTransaction())
                    {
                        res = action(session);
                        tx.Commit();
                    }
                    return res;
                });
        }

        public static T ExceptionFunction<T>(Func< T> action)
        {
            T res;
			try
			{
				res = action();
			}
			catch (Exception ex)
			{
				var sqlException = ex.InnerException as SqlException;
				if (sqlException == null)
				{
					sqlException = ex as SqlException;
				}
				if (sqlException == null)
				{
					throw ex;
				}
				else if (sqlException != null && sqlException.Class > 16)
				{
					throw new ConnectException(sqlException);
				}
				else
				{
					throw new OperationException(sqlException);
				}
			}
            return res;
        }
    
        public  static T Function<T>(this ISessionCreator factory, Func<T> action)
        {
            return ExceptionFunction(action);
        }
    }

    
}
