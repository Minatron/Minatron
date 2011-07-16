using System;
namespace ImagesStoreSystem.DBProvider.Core
{
    public class BackupFile : RemovableObject
    {
        protected BackupFile()
        {
        }

        public BackupFile(DataFile file, BackupsStorage storage)
        {
            if (file == null) throw new ArgumentNullException("file");
            if (storage == null) throw new ArgumentNullException("storage");
            file.Backups.Add(this);
            File = file;
            Storage = storage;
            Time = DateTime.UtcNow;
        }

        public virtual DataFile File { get; protected set; }
        public virtual BackupsStorage Storage { get; protected set; }
        public virtual DateTime Time { get; protected set; }


    }
}

