using System;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Model;
using System.Collections.ObjectModel;

namespace StorageModule.Services
{
	public static class DictionaryRepositoryExtension
	{
		public static void DeleteProxy<T>(this DictionaryRepository<T> repo, DictionaryItemProxy proxy) where T : DictionaryBase
		{
			try
			{
				repo.Remove(proxy.Item as T);
			}
			catch(Exception ex)
			{
				//TODO удаляем объект: которого уже нет в БД
				throw ex;
			}
		}

		public static void SaveProxy<T>(this DictionaryRepository<T> repo, DictionaryItemProxy proxy) where T : DictionaryBase
		{
			proxy.Save(repo.GetSameObject(proxy.Item as T));
		}

		public static void SaveProxyAsNew<T>(this DictionaryRepository<T> repo, DictionaryItemProxy proxy) where T : DictionaryBase, new()
		{
			var obj = new T();
			if (proxy.Save(obj))
			{
				repo.Save(obj);
			}
		}

		public static void SaveProxy<T>(this DictionaryRepository<T> repo, DictionaryProxy proxy) where T : DictionaryBase
		{

				foreach (var item in proxy.GetDeletedFiles())
				{
					DeleteProxy<T>(repo, item);
				}
				foreach (var item in proxy.Collection)
				{
					SaveProxy<T>(repo, item);
					repo.SaveAllChanges();
				}
		}

		public static void RefreshProxy<T>(this DictionaryRepository<T> repo, DictionaryProxy proxy) where T : DictionaryBase
		{
			proxy.Reset(repo.GetAll());
		}

		public static void RefreshDictionary<T>(this DictionaryRepository<T> repo, ObservableCollection<DictionaryBase> collection) where T : DictionaryBase
		{
			collection.Clear();
			foreach (var item in repo.GetAll())
			{
				collection.Add(item);
			}
		}
	}
}
