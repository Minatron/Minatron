using System;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Model;
using System.Collections.ObjectModel;

namespace StorageModule.Services
{
	public interface IDictionaryService
	{
		DictionaryProxy CreateDictionaryProxy();
		void Refresh(DictionaryProxy proxy);
		void Save(DictionaryProxy proxy);
		void SaveAsNew(DictionaryItemProxy proxy);
		DictionaryItemProxy CreateItemProxy();
		ObservableCollection<DictionaryBase> CreateDictionary();
		void Refresh(ObservableCollection<DictionaryBase> collection);
		DictionaryBase GetBy(long catalogNumber);
	}

	public class DictionaryService<T> : IDictionaryService where T : DictionaryBase, new()
	{
		protected DictionaryRepository<T> _repo;

		protected DictionaryService() { }
		public DictionaryService(StorageService storage)
		{
			if (storage == null) throw new ArgumentNullException("storage");
			_repo = storage.CreateDictionaryRepository<T>();
		}

		public DictionaryItemProxy CreateItemProxy()
		{
			return DictionaryItemProxy.Create(new T());
		}

		public DictionaryProxy CreateDictionaryProxy()
		{
			var proxy = new DictionaryProxy();
			Refresh(proxy);
			return proxy;
		}

		public ObservableCollection<DictionaryBase> CreateDictionary()
		{
			var dictionary = new ObservableCollection<DictionaryBase>();
			Refresh(dictionary);
			return dictionary;
		}

		public void Refresh(DictionaryProxy proxy)
		{
			_repo.RefreshProxy(proxy);
		}

		public void Refresh(ObservableCollection<DictionaryBase> collection)
		{
			_repo.RefreshDictionary(collection);
		}

		public void SaveAsNew(DictionaryItemProxy proxy)
		{
			_repo.SaveProxyAsNew(proxy);
		}

		public void Save(DictionaryProxy proxy)
		{
			_repo.SaveProxy(proxy);
		}
	
		public virtual DictionaryBase GetBy(long catalongNumber)
		{
			return null;
		}
	}

	public class NumberedDictionaryService<T> : DictionaryService<T> where T : NumberedDictionaryBase, new()
	{
		public NumberedDictionaryService(StorageService storage)
		{
			if (storage == null) throw new ArgumentNullException("storage");
			_repo = storage.CreateNumberedDictionaryRepository<T>();
		}

		public override DictionaryBase GetBy(long catalogNumber)
		{
			return (_repo as NumberedDictionaryRepository<T>).GetBy(catalogNumber);
		}
	}
}
