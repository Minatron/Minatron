using NHibernate;
using NHibernate.Criterion;

namespace ImagesStoreSystem.DBProvider.Core.Extension
{
    public static class GetOne
    {
        public static T SameObject<T>(ISession session, T obj) where T : UpdatableObject
        {
            if (obj == null) return null;
            return session.Load<T>(obj.Id);
        }

		public static T DictionaryItem<T>(ISession session, long catalogNumber) where T : NumberedDictionaryBase
        {
            return session.CreateCriteria<T>().Add(Restrictions.Eq("CatalogNumber", catalogNumber)).UniqueResult<T>();
        }

		public static Satellite GetSatellite(this Image img, ISession session)
		{
			if (!img.SatelliteCatalogNumber.HasValue) return null;
			return GetOne.DictionaryItem<Satellite>(session, img.SatelliteCatalogNumber.Value);
		}

        public static Station GetStation(this ReceiveSession obj, ISession session)
        {
			return GetOne.DictionaryItem<Station>(session, obj.StationCatalogNumber);
        }

        public static Satellite GetSatellite(this ReceiveSession obj, ISession session)
        {
			return GetOne.DictionaryItem<Satellite>(session, obj.SatelliteCatalogNumber);
        }

        public static Station GetStation(this ReceivePlanTask obj, ISession session)
        {
			return GetOne.DictionaryItem<Station>(session, obj.StationCatalogNumber);
        }

        public static Satellite GetSatellite(this ReceivePlanTask obj, ISession session)
        {
			return GetOne.DictionaryItem<Satellite>(session, obj.SatelliteCatalogNumber);
        }
    }
}
