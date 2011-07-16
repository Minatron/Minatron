using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using ImagesStoreSystem.DBProvider.Core;

using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace CommonObjects
{
	public enum PageWarning : int
	{
		AllRight = 0,
		NoObjectsInDB = 1,
		NoObjectsForThisFilter = 2
	}

	public abstract class PagePresenter<T> : Presenter where T : IdentifiableObject
	{
		#region Переменные

		protected int _pageNumber;
		int _pageCapacity;
		bool _hasNextPage = false;
		bool _hasObjects = false;

		#endregion Переменные


		PageWarning _warning = PageWarning.AllRight;

		public PageWarning Warning
		{
			get
			{
				return _warning;
			}
			protected set
			{
				_warning = value;
				OnPropertyChanged("Warning");
			}
		}



		#region Свойства

		public bool HasObjects
		{
			get
			{
				return _hasObjects;
			}
			protected set
			{
				_hasObjects = value;
				OnPropertyChanged("HasObjects");
			}
		}

		public bool HasNextPage
		{
			get
			{
				return _hasNextPage;
			}
			protected set
			{
				_hasNextPage = value;
				OnPropertyChanged("HasNextPage");
			}
		}

		public bool HasPrevPage
		{
			get
			{
				return _pageNumber > 0;
			}
		}

		public int PageNumber
		{
			get
			{
				return _pageNumber;
			}
			set
			{
				if (_pageNumber != value)
				{
					if (!HasNextPage)
					{
						value = Math.Min(value, _pageNumber);
					}
					_pageNumber = Math.Max(0, value);
					OnPropertyChanged("PageNumber");
					OnPropertyChanged("HasPrevPage");
					OnPropertyChanged("Objects");
				}
			}
		}

		public int PageCapacity
		{
			get
			{
				return _pageCapacity;
			}
			set
			{
				PageCapacity = value;
				OnPropertyChanged("PageCapacity");
				RefreshCommand.Execute(null);
			}
		}

		#endregion Свойства



		#region Комманды

		public ICommand NextPageCommand { get; protected set; }
		public ICommand PrevPageCommand { get; protected set; }
		public ICommand RefreshCommand { get; protected set; }

		void InitCommands()
		{
			NextPageCommand = new DelegateCommand<object>((o) => ++PageNumber);
            PrevPageCommand = new DelegateCommand<object>((o) => --PageNumber);
            RefreshCommand = new DelegateCommand<object>((o) => Refresh());
		}

		protected virtual void Refresh()
		{
			OnPropertyChanged("Objects");
		}

		#endregion Комманды



		#region Конструкторы

		public PagePresenter(IEventAggregator eventAggregator, int pageCapacity = 10)
			: base(eventAggregator)
		{
			_pageNumber = 0;
			_pageCapacity = pageCapacity;

			InitCommands();
		}

		#endregion Конструкторы

		public abstract IList<T> Objects { get; }

		protected virtual IList<T> RefreshWarningState(IList<T> objectsOnPage)
		{
			int count = objectsOnPage.Count;
			if (count == 0)
			{
				HasNextPage = false;
				if (PageNumber == 0)
				{
					HasObjects = false;
				}
				else
				{
					--_pageNumber;
					objectsOnPage = Objects;
					HasNextPage = false;
					OnPropertyChanged("PageNumber");
				}
			}
			else
			{
				Warning = PageWarning.AllRight;
				HasObjects = true;
				HasNextPage = count == PageCapacity && count > 0;
			}
			OnPropertyChanged("HasPrevPage");
			return objectsOnPage;
		}
	}
}