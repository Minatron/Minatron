using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using StorageModule.Model;
using StorageModule.Services;

namespace DictionaryModule.Presenters
{
	public class DictionaryProxyPresenter : INotifyPropertyChanged
	{
		readonly IDictionaryService _service;
		readonly DictionaryProxy _model;

		public DictionaryType InfoType { get; protected set; }
		public string ErrorMessage { get; protected set; }

		public DictionaryProxyPresenter(IDictionaryService service, DictionaryType infoType)
		{
			if (service == null) throw new ArgumentNullException("service");
			_service = service;
			_model = _service.CreateDictionaryProxy();
			InfoType = infoType;

			DeleteCommand = new DelegateCommand<IDeletable>(DeleteItem);
			RefreshCommand = new DelegateCommand<object>(Refresh);
			ApplyCommand = new DelegateCommand<object>(Apply);
			SaveCommand = new DelegateCommand<object>(Save);
			CancelCommand = new DelegateCommand<object>(Close);
		}

		public ObservableCollection<DictionaryItemProxy> Collection
		{
			get
			{
				return _model.Collection;
			}
		}

		public ICommand DeleteCommand { get; protected set; }

		public ICommand SaveCommand { get; protected set; }
		public ICommand RefreshCommand { get; protected set; }
		public ICommand ApplyCommand { get; protected set; }
		public ICommand CancelCommand { get; protected set; }
		

		public Action OnClose = null;

		void DeleteItem(IDeletable item)
		{
			if (item != null)
			{
				item.IsDeleted = true;
			}
		}

		public void Refresh(object obj = null)
		{
			_service.Refresh(_model);
			ErrorMessage = null;
			OnPropertyChanged("ErrorMessage");
		}

		void Save(object obj = null)
		{
			try
			{
				_service.Save(_model);
				ErrorMessage = null;
			}
			catch
			{
				ErrorMessage = "Error";
			}
			OnPropertyChanged("ErrorMessage");
		}

		void Apply(object obj = null)
		{
			Save();
			if (ErrorMessage == null)  Close();
		}

		void Close(object obj =null)
		{
			if (OnClose != null) OnClose();
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
