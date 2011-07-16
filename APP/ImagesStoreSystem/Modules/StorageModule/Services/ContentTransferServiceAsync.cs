using System;
using System.Linq;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Model;
using System.Threading;
using System.Collections.Generic;

namespace StorageModule.Services
{

	public class ContentTransferServiceAsync : ContentTransferService
	{
		#region sync

		volatile object _asyncRoot = new object();
		bool _isBusy = false;

		protected bool Take()
		{
			lock (_asyncRoot)
			{
				if (!_isBusy) _isBusy = true; else return false;
			}
			return true;
		}

		protected void Release()
		{
			lock (_asyncRoot)
			{
				_isBusy = false;
			}
		}

		#endregion

		DataFileRepository _asyncRepo;

		public ContentTransferServiceAsync(Properties.Settings setting, StorageService storage) : base(setting, storage) 
		{
			_asyncRepo = storage.CreateDataFileRepository();
		}

		public bool SaveAsync<T>(FileCollectionProxy proxy, T dstObject, Action<Exception> onEnd = null) where T : UpdatableWithPacketObject
		{
			if (!Take()) return false;
			if (!ThreadPool.QueueUserWorkItem(
				o =>
				{
					Exception e = null;
					try
					{
						_asyncRepo.SaveProxy<T>(proxy, dstObject, Watcher);
					}
					catch(Exception ex)
					{
						e = ex;
					}
					Release();
					if (onEnd != null) onEnd(e);
				}))
			{
				Release();
				return false;
			}
			return true;
		}

		public bool DownloadAsync(IEnumerable<IFileInfo> fileProxies, string rootPath, Action<Exception> onEnd = null)
		{
			if (!Take()) return false;
			if (!ThreadPool.QueueUserWorkItem(
				o =>
				{
					Exception e = null;
					try
					{
						_asyncRepo.DownloadFiles(fileProxies, rootPath, Watcher);
					}
					catch(Exception ex)
					{
						e = ex;
					}
					Release();
					if (onEnd != null) onEnd(e);
				}))
			{
				Release();
				return false;
			}
			return true;
		}
	}
}
