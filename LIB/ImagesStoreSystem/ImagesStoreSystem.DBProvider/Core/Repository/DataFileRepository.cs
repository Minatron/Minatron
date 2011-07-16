using System;
using System.Collections.Generic;
using System.IO;
using ImagesStoreSystem.DBProvider.Core.Extension;

namespace ImagesStoreSystem.DBProvider.Core
{
    public class DataFileRepository : UpdatableRepository<DataFile>
    {
        public DataFileRepository(ISessionCreator factory)
            : base(factory)
        { }

        public void UploadContent(DataFile file, Stream src)
        {
            _factory.TransactionAction(s => file.UploadContent(s, src));
        }


        public void DownloadContent(DataFile file, Stream dst)
        {
            _factory.TransactionAction(s => file.DownloadContent(s, dst));
        }
        public void RemoveContent(DataFile file)
        {
            _factory.TransactionAction(s => file.RemoveContent(s));
        }

        public BackupFile CreateBackup(DataFile file, BackupsStorage storage)
        {
            return _factory.TransactionFunction(s => file.CreateBuckup(s, storage));
        }

        public IList<DataFile> GetFiles<T>(T obj) where T : UpdatableWithPacketObject
        {            
            return _factory.Function(() => GetAll.DataFiles(GetNewSession(), obj));
        }

        public void UploadFile<T>(T obj, INewFileInfo info) where T : UpdatableWithPacketObject
        {
            UploadFiles<T>(obj, new INewFileInfo[] { info });
        }

        public void UploadFiles<T>(T obj, IEnumerable<INewFileInfo> fileInfos, Action<long> watcher = null) where T : UpdatableWithPacketObject
        {
            _factory.TransactionAction(
                s =>
                {
                    var packet = obj.GetPacket<T>(s);
					
                    long uploadedSize = 0;
                    foreach (var info in fileInfos)
                    {
                        var file = new DataFile(packet, info);
                        file = file.Save(s);
                        using (var stream = new FileStream(info.SourcePath,FileMode.Open,FileAccess.Read, FileShare.ReadWrite))
                        {
                            file.UploadContent(s, stream);
                        }
                        uploadedSize += info.Size;
                        if (watcher != null)
                        {
                            watcher(uploadedSize);
                        }
                    }
                });
        }

		public void DownloadFiles(IEnumerable<DataFile> files, string dstPath, Action<long> watcher = null)
		{
			if (!Directory.Exists(dstPath)) Directory.CreateDirectory(dstPath);
			long downloadSize = 0;

			_factory.TransactionAction(
				s =>
				{
					foreach (var file in files)
					{
						using (var stream = File.Create(string.Format("{0}{1}{2}", dstPath, Path.DirectorySeparatorChar, file.Name)))
						{
							file.DownloadContent(s, stream);
							downloadSize += file.Size;
							if (watcher != null) watcher(downloadSize);
						}
					}
				});
		}
    }
}
