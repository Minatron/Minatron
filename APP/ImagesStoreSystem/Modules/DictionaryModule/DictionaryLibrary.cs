using System;
using System.Collections.Generic;
using DictionaryModule.Presenters;
using DictionaryModule.Views;
using ImagesStoreSystem.DBProvider.Core;
using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Events;
using StorageModule.Model;
using StorageModule.Services;

namespace DictionaryModule
{
	
	public class DictionaryLibrary
	{
		readonly IEventAggregator _eventAggregator;
		readonly ModalViewManager _viewManager;
		readonly DictionaryProxyEditorView _editorView;
		readonly CreateDictionaryItemView _createView;

		Dictionary<DictionaryType, CreateDictionaryItemPresenter> _createPresenters = new Dictionary<DictionaryType, CreateDictionaryItemPresenter>();
		Dictionary<DictionaryType, DictionaryProxyPresenter> _editorPresenters = new Dictionary<DictionaryType, DictionaryProxyPresenter>();
		Dictionary<DictionaryType, DictionaryModel> _dictonaryModels = new Dictionary<DictionaryType, DictionaryModel>();

		public DictionaryLibrary(StorageService storage, IEventAggregator eventAggregator, ModalViewManager viewManager)
		{
			_eventAggregator = eventAggregator;
			_viewManager = viewManager;


			_editorView = new DictionaryProxyEditorView(null);
			_createView = new CreateDictionaryItemView(null);

			Dictionary<DictionaryType, IDictionaryService> services = new Dictionary<DictionaryType, IDictionaryService>();

			
			services.Add(DictionaryType.AdditionalField, new DictionaryService<AttributeTitle>(storage));
			services.Add(DictionaryType.Station, new NumberedDictionaryService<Station>(storage));
			services.Add(DictionaryType.ImageLevel, new DictionaryService<ImageLevel>(storage));
			services.Add(DictionaryType.Satellite, new NumberedDictionaryService<Satellite>(storage));
			services.Add(DictionaryType.Sensor, new DictionaryService<SatelliteSensor>(storage));
			services.Add(DictionaryType.BackupStorage, new DictionaryService<BackupsStorage>(storage));

			foreach (var elem in services)
			{
				_dictonaryModels.Add(elem.Key, new DictionaryModel(elem.Value, elem.Key));
				_createPresenters.Add(elem.Key, new CreateDictionaryItemPresenter(elem.Value, elem.Key));
				_editorPresenters.Add(elem.Key, new DictionaryProxyPresenter(elem.Value, elem.Key));
			}
		}

		public void ShowEditView(DictionaryType type)
		{
			var presenter = _editorPresenters[type];
			presenter.Refresh();
			presenter.OnClose =
				() =>
				{
					if (presenter.ErrorMessage == null)
					{
						_dictonaryModels[type].Refresh();
						_eventAggregator.GetEvent<RefreshAllEvent>().Publish(null);
					}
					_viewManager.Hide();
				};
			_editorView.DataContext = presenter;
			_viewManager.Show(_editorView);
		}

		public void ShowCreateView(DictionaryType type, Action<DictionaryBase> afterCreate = null, long catalogNumber = 0)
		{
			var presenter = _createPresenters[type];
			presenter.Clear();
			presenter.OnClose =
				() =>
				{
					_eventAggregator.GetEvent<RefreshAllEvent>().Publish(null);
					if (afterCreate != null && presenter.ErrorMessage == null)
					{
						_dictonaryModels[type].Refresh();
						afterCreate(presenter.Proxy.Item);
					}
					_viewManager.Hide();
				};
			var item = presenter.Proxy as NumberedDictionaryItemProxy;
			if (item != null) item.CatalogNumber = catalogNumber;
			_createView.DataContext = presenter;
			_viewManager.Show(_createView, ModalViewType.Center);
		}

		public DictionaryPresenter CreatePresenter(DictionaryType type, bool isReadonly = false)
		{
			return new DictionaryPresenter(_dictonaryModels[type], this, isReadonly);
		}
	}
}
