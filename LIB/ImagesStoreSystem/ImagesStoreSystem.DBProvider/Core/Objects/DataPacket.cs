using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace ImagesStoreSystem.DBProvider.Core
{
    public class DataPacket : IdentifiableObject
    {
        internal DataPacket()
        {
            Files = new List<DataFile>();
        }

        public virtual IList<DataFile> Files { get; protected set; }
    }
}
