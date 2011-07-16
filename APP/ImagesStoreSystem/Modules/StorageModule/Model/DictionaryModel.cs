using System;
using System.Collections.ObjectModel;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Services;
using System.Linq;

namespace StorageModule.Model
{
	public enum DictionaryType
	{
		Station,
		Satellite,
		ImageLevel,
		Sensor,
		BackupStorage,
		AdditionalField
	}

	public class DictionaryModel 
	{
		public DictionaryType Type { get; private set; }

		readonly protected IDictionaryService _service;

		public event Action OnRefresh;

		public ObservableCollection<DictionaryBase> Collection { get; private set; }

		public DictionaryModel(IDictionaryService service, DictionaryType type)
		{
			Type = type;
			_service = service;
			Collection = _service.CreateDictionary();
		}

		public void Refresh()
		{
			_service.Refresh(Collection);
			if (OnRefresh != null) OnRefresh();
		}

		public DictionaryBase GetBy(long? catalogNumber)
		{
			if (!catalogNumber.HasValue) return null;
			return GetBy(_service.GetBy(catalogNumber.Value));
		}

		public DictionaryBase GetBy(DictionaryBase obj)
		{
			if (obj == null) return null;
			return Collection.FirstOrDefault(o => o.Id == obj.Id);
		}
	}
}
