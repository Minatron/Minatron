using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ImagesStoreSystem.DBProvider.Core;
using Microsoft.Practices.Composite.Presentation.Commands;
using StorageModule.Model;
using System;

namespace DictionaryModule.Presenters
{
	public class DictionaryPresenter : INotifyPropertyChanged
	{
		readonly DictionaryModel _model;
		readonly DictionaryLibrary _lib;
		DictionaryBase _selectedItem;
		long? _catalogNumber;
		ObservableCollection<DictionaryBase> _collection = new ObservableCollection<DictionaryBase>();

		public bool IsReadonly { get; private set; }
		public bool IsNumbered
		{
			get
			{
				return _model.Type == DictionaryType.Station || _model.Type == DictionaryType.Satellite;
			}
		}

		public Action OnSelectedChanged;
		void OnSelectedChangedExecute()
		{
			if (OnSelectedChanged != null)
			{
				OnSelectedChanged();
			}
		}

		internal DictionaryPresenter(DictionaryModel model, DictionaryLibrary lib, bool isReadonly = true)
		{
			_lib = lib;
			_model = model;
			_model.OnRefresh += OnRefreshModel;
			IsReadonly = isReadonly;
			AddCommand = new DelegateCommand<object>(Add);

			CopyFromModelToCollection();
		}

		void CopyFromModelToCollection()
		{
			_collection.Clear();
			foreach (var item in _model.Collection)
			{
				_collection.Add(item);
			}
		}

		public ICommand AddCommand { get; protected set; }

		void OnRefreshModel()
		{
			var catN = _catalogNumber;
			var selItem = _selectedItem;

			CopyFromModelToCollection();

			if (catN.HasValue)
			{
				SelectedItem = _model.GetBy(catN);
			}
			else if (selItem != null)
			{
				SelectedItem = _model.Collection.FirstOrDefault(el => el.Id == selItem.Id);
			}
		}

		public void Refresh()
		{
			_model.Refresh();
		}

		public ObservableCollection<DictionaryBase> Dictionary
		{
			get
			{
				return _collection;
			}
		}

		public long? CatalogNumber
		{
			get
			{
				return _catalogNumber;
			}
			set
			{
				_catalogNumber = value;
				_selectedItem = _model.GetBy(_catalogNumber);
				OnPropertyChanged("SelectedItem");
				OnPropertyChanged("CatalogNumber");
				OnSelectedChangedExecute();
			}
		}

		public DictionaryBase SelectedItem
		{
			get
			{
				return _selectedItem;
			}
			set
			{
				_selectedItem =  _model.GetBy(value);
				var numberedValue = _selectedItem as NumberedDictionaryBase;
				if (numberedValue != null)
				{
					_catalogNumber = numberedValue.CatalogNumber;
				}
				else
				{
					_catalogNumber = null;
				}
				OnPropertyChanged("CatalogNumber");
				OnPropertyChanged("SelectedItem");
				OnSelectedChangedExecute();
			}
		}

		void Add(object arg = null)
		{
			if (!IsReadonly)
			{
				_lib.ShowCreateView(_model.Type, item =>
					{
						SelectedItem = item;
					}, (SelectedItem == null && CatalogNumber.HasValue) ? CatalogNumber.Value : 0);
			}
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
