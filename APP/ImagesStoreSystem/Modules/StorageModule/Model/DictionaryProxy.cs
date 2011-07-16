using System.Collections.Generic;
using System.Collections.ObjectModel;
using ImagesStoreSystem.DBProvider.Core;

namespace StorageModule.Model
{
	public class DictionaryProxy
	{
		public ObservableCollection<DictionaryItemProxy> Collection { get; protected set; }

		public DictionaryProxy()
		{
			Collection = new ObservableCollection<DictionaryItemProxy>();
		}

		public void Clear()
		{
			Collection.Clear();
		}

		public void Reset<T>(IList<T> items) where T : DictionaryBase
		{
			Clear();
			foreach (var item in items)
			{
				Collection.Add(DictionaryItemProxy.Create(item));
			}
		}

		public IList<DictionaryItemProxy> GetDeletedFiles()
		{
			var res = new List<DictionaryItemProxy>();
			foreach (var item in Collection)
			{
				if (item.IsDeleted)
				{
					res.Add(item);
				}
			}
			return res;
		}
	}
}
