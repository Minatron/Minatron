using System;
using System.Collections.Generic;
using System.Linq;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Model;

namespace StorageModule.Services
{
	public static class DataFileRepositoryExtension
	{
		public static void DeleteProxy(this DataFileRepository repo, EditFileProxy proxy)
		{
			try
			{
				repo.Remove(proxy.File);
			}
			catch (Exception ex)
			{
				//TODO удаляем файл, которого уже нет в БД. Надо ингорировать...
				throw ex;
			}
		}

		public static void SaveProxy(this DataFileRepository repo, EditFileProxy proxy)
		{
			proxy.Save(repo.GetSameObject(proxy.File));
		}

		public static void SaveProxy<T>(this DataFileRepository repo, FileCollectionProxy proxy, T dstObject, ContentTransferWatcher watcher) where T : UpdatableWithPacketObject
		{
			foreach (var file in proxy.GetDeletedFiles())
			{
				DeleteProxy(repo, file);
			}
			foreach (var file in proxy.Collection)
			{
				var editProxyFile = file as EditFileProxy;
				if (editProxyFile != null)
				{
					SaveProxy(repo, editProxyFile);
					repo.SaveAllChanges();
				}
			}

			var newFiles = proxy.GetNewFiles();
			if (newFiles.Count > 0)
			{
				long totalSize = newFiles.Sum(p => p.Size);
				watcher.Start(totalSize, OperationType.Upload);
                Exception ex = null;
                try
                {
                    repo.UploadFiles(dstObject, newFiles, watcher.Elapsed);
                }
                catch (Exception e)
                {
                    ex = e;                    
                }
                watcher.End(ex);
			}
		}

		public static void RefreshProxy<T>(this DataFileRepository repo, FileCollectionProxy proxy, T srcObject) where T : UpdatableWithPacketObject
		{
			if (srcObject != null) proxy.Reset(repo.GetFiles<T>(srcObject));
			else proxy.Clear();
		}

		public static void DownloadFiles(this DataFileRepository repo, IEnumerable<IFileInfo> fileProxies, string rootPath, ContentTransferWatcher watcher) 
		{
			var files = new List<DataFile>();
			long totalSize = 0;
			foreach (var proxy in fileProxies)
			{
				if (proxy is EditFileProxy)
				{
					files.Add((proxy as EditFileProxy).File);
					totalSize += proxy.Size;
				}
			}
			watcher.Start(totalSize, OperationType.Download);
            Exception ex = null;
            try
            {
                repo.DownloadFiles(files, rootPath, watcher.Elapsed);
            }
            catch (Exception e)
            {
                ex = e;
            }
            watcher.End(ex);
		}
	}
}
