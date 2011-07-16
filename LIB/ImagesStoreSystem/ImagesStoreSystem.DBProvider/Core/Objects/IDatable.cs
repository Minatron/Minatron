
namespace ImagesStoreSystem.DBProvider.Core
{
	public abstract class UpdatableWithPacketObject : UpdatableObject
	{
		public virtual long? PacketID { get; set; }
	}
}
