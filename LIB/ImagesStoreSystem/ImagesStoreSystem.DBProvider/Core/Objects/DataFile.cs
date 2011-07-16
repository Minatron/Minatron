using System;
using Iesi.Collections.Generic;
namespace ImagesStoreSystem.DBProvider.Core
{

    public class DataFile : UpdatableObject
    {

        protected DataFile()
        {
            Backups = new HashedSet<BackupFile>();
        }
        public DataFile(DataPacket pack, long size)
            : this()
        {
            if (pack == null) throw new ArgumentNullException("pack");
            pack.Files.Add(this);
            Packet = pack;
            Size = size;
        }

        public DataFile(DataPacket pack, INewFileInfo info)
            : this(pack, info.Size)
        {
            Name = info.Name;
            Description = info.Description;
            TypeInfo = info.TypeInfo;
        }

        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual string Name { get; set; }
        public virtual FileInfoType TypeInfo { get; set; }

        public virtual Guid GUID { get; protected set; }
        public virtual long Size { get; protected set; }
        public virtual DateTime Time { get; protected set; }

        public virtual string Description { get; set; }

        public virtual DataPacket Packet { get; protected set; }
        public virtual ISet<BackupFile> Backups { get; protected set; }
    }
}
