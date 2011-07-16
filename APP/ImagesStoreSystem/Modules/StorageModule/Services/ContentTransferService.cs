using System;
using StorageModule.Model;
using ImagesStoreSystem.DBProvider.Core;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.ComponentModel;

namespace StorageModule.Services
{
	public enum OperationType
	{
		Upload,
		Download
	}

	public abstract class ContentTransferService : INotifyPropertyChanged
	{
		DataFileRepository _repo;

		public ContentTransferWatcher Watcher { get; private set; }

		#region Speed
		
		protected static long CalcMeanSpeed(long lastValue, long newValue)
		{
			return Math.Max(0, (long)(0.9 * lastValue + 0.1 * newValue));
		}

		protected Properties.Settings _settings;

		public void SetDownloadSpeed(long value)
		{
			_settings.DownloadSpeed = CalcMeanSpeed(_settings.DownloadSpeed, value);
			_settings.Save();
		}

		public long GetDownloadSpeed()
		{
			return _settings.DownloadSpeed;
		}

		public void SetUploadSpeed(long value)
		{
			_settings.UploadSpeed = CalcMeanSpeed(_settings.UploadSpeed, value);
			_settings.Save();
		}

		public long GetUploadSpeed()
		{
			return _settings.UploadSpeed;
		}

		#endregion

		public ContentTransferService(Properties.Settings settings, StorageService storage)
		{
			if (settings == null) throw new ArgumentNullException("settings");
			if (storage == null) throw new ArgumentNullException("storage");

			_repo = storage.CreateDataFileRepository();
			_settings = settings;

			Watcher = new ContentTransferWatcher(this);
		}

		public FileCollectionProxy GetCollectionProxy<T>(T owner) where T : UpdatableWithPacketObject
		{
			var proxy = new FileCollectionProxy();
			Refresh(proxy, owner);
			return proxy;
		}

		public void Refresh<T>(FileCollectionProxy proxy, T srcObject) where T : UpdatableWithPacketObject
		{
			_repo.RefreshProxy<T>(proxy, srcObject);

		}

		public void Save<T>(FileCollectionProxy proxy, T dstObject) where T : UpdatableWithPacketObject
		{
			_repo.SaveProxy<T>(proxy, dstObject, Watcher);

		}

		public void Download(IEnumerable<IFileInfo> fileProxies, string rootPath)
		{
			_repo.DownloadFiles(fileProxies, rootPath, Watcher);
		}

		#region INotifyPropertyChanged

		void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null) return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion INotifyPropertyChanged
	}
}
