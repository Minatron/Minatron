using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace ImagesStoreSystem.DBProvider.Core.Extension
{
    public static class GetAll
    {

        public static IList<Image> Images(ISession session, IEnumerable<IImageFilter> filters)
        {
            ICriteria criteria = session.CreateCriteria<Image>();
            foreach (IImageFilter filter in filters)
            {
                criteria = filter.AddCriteria(criteria);
            }
            return criteria.List<Image>();
        }


        public static IList<ReceiveSession> ReceiveSessions(ISession session, IEnumerable<IReceiveSessionFilter> filters)
        {
            ICriteria criteria = session.CreateCriteria<ReceiveSession>();
            foreach (IReceiveSessionFilter filter in filters)
            {
                criteria = filter.AddCriteria(criteria);
            }
            return criteria.List<ReceiveSession>();
        }

        public static IList<T> Dictonary<T>(ISession session) where T : DictionaryBase
        {
            return session.CreateCriteria<T>().AddOrder(Order.Asc("Title")).SetCacheable(true).List<T>();
        }

        public static IList<ReceivePlanTask> ReceivePlanTasks(ISession session, IEnumerable<IReceivePlanTaskFilter> filters)
        {
            ICriteria criteria = session.CreateCriteria<ReceivePlanTask>();
            foreach (IReceivePlanTaskFilter filter in filters)
            {
                criteria = filter.AddCriteria(criteria);
            }
            return criteria.List<ReceivePlanTask>();
        }

		public static IList<DataFile> DataFiles<T>(ISession session, T obj) where T : UpdatableWithPacketObject
		{            
            var packet = obj.GetPacket(session);
            if (packet != null)
            {
                return packet.Files;
            }
            else
            {
                return new DataFile[0];
            }              
		}
    }
}
