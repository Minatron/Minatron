using NHibernate;

namespace ImagesStoreSystem.DBProvider.Core.Extension
{
    public static class RemoveExtension
    {
        public static void RemoveFrom<T>(this T obj, ISession session) where T : RemovableObject
        {
			var prisoner = session.Get<T>(obj.Id);
			var pahan = prisoner as UpdatableWithPacketObject;
			DataPacket avtoritet = null;
			if (pahan != null && pahan.PacketID.HasValue)
			{
				avtoritet = session.Get<DataPacket>(pahan.PacketID);
			}
			session.Delete(prisoner);
			if (avtoritet != null)
			{
				session.Delete(avtoritet);
			}
			session.Flush();
		
        }
    }
}
