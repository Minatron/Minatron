using System;
using System.ComponentModel;
using System.Windows.Input;
using ImagesStoreSystem.DBProvider.Core;
using LocalizationLibrary;
using Microsoft.Practices.Composite.Presentation.Commands;
using StorageModule.Model;
using StorageModule.Services;

namespace DictionaryModule.Presenters
{
	public class CreateDictionaryItemPresenter : INotifyPropertyChanged
	{
		readonly IDictionaryService _service;
		readonly DictionaryItemProxy _model;

		public DictionaryType InfoType { get; protected set; }
		public string ErrorMessage { get; protected set; }

		public CreateDictionaryItemPresenter(IDictionaryService service, DictionaryType infoType)
		{
			if (service == null) throw new ArgumentNullException("service");
			_service = service;
			_model = _service.CreateItemProxy();
			InfoType = infoType;

			SaveCommand = new DelegateCommand<object>(Save);
            ClearCommand = new DelegateCommand<object>(Clear);
			ApplyCommand = new DelegateCommand<object>(Apply);
			CancelCommand = new DelegateCommand<object>(Close);
		}

		public DictionaryItemProxy Proxy
		{
			get
			{
				return _model;
			}
		}

		public ICommand SaveCommand { get; protected set; }
		public ICommand ClearCommand { get; protected set; }
		public ICommand ApplyCommand { get; protected set; }
		public ICommand CancelCommand { get; protected set; }

		public Action OnClose = null;

		void Save(object arg = null)
		{
			try
			{
				_service.SaveAsNew(_model);
				ErrorMessage = null;
			}
			catch (OperationException)
			{
				ErrorMessage = Lang.GetTitle("Modules/Dictionary/EditObjectView/AlreadyExist");
			}
			OnPropertyChanged("ErrorMessage");
		}

		void Apply(object arg)
		{
			Save();
			if (ErrorMessage == null) Close();			
		}

		void Close(object arg = null)
		{
			if (OnClose != null) OnClose();
		}

		public void Clear(object arg = null)
		{
			ErrorMessage = null;
			OnPropertyChanged("ErrorMessage");
            _model.Clear();
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
