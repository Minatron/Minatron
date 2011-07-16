using NHibernate;

namespace ImagesStoreSystem.DBProvider.Core.Extension
{
    public static class SetupPacketExtension
    {
        private static DataPacket CreateDataPacket(ISession session)
        {
            var obj = new DataPacket();
			return session.SaveOrUpdateCopy(obj) as DataPacket;
        }

		public static DataPacket GetPacket<T>(this T src, ISession session) where T : UpdatableWithPacketObject
		{

            DataPacket packet = null;
            long id = src.Id;
            var dst = session.Get<T>(id);

            if (dst.PacketID.HasValue)
            {
                packet = session.Get<DataPacket>(dst.PacketID);
            }
            else
            {
                packet = CreateDataPacket(session);
                dst.PacketID = packet.Id;
                session.Flush();
            }
            return packet;
 
		}
    }
}
