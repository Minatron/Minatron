using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ImagesStoreSystem.DBProvider.Core;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace CommonObjects
{
	public abstract class FilterManagerBase<T> : IFiltersManager<T>, INotifyPropertyChanged where T : IStorageFilter
	{
		bool _wasChanged = false;

		public bool WasChanged
		{
			get
			{
				return _wasChanged;
			}
			protected set
			{
				_wasChanged = value;
				OnPropertyChanged("WasChanged");
			}
		}

		public IList<T> Filters { get; protected set; }

		public FilterManagerBase()
		{
			Filters = new List<T>();
		}

		public abstract IList<T> RecreateActiveFilters();
	
		#region INotifyPropertyChanged

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null) return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion INotifyPropertyChanged
	}

	public abstract class FiltersManagerPresenter<T> : FilterManagerBase<T>, ICollectionFiltersManager<T> where T : IStorageFilter
	{
		public ObservableCollection<FilterPresenter<T>> RegisteredFilters {get; protected set;}
		public ObservableCollection<FilterPresenter<T>> NotUsedFilters { get; protected set; }

		public ICommand AddCommand { get; protected set; }
		public ICommand RefreshCommand { get; protected set; }

		public bool HasActiveFilters
		{
			get
			{
				foreach (var filter in RegisteredFilters)
				{
					if (filter.IsActive)
					{
						return true;
					}
				}
				return false;
			}
		}

		public bool HasNotActiveFilters
		{
			get
			{
				foreach (var filter in RegisteredFilters)
				{
					if (!filter.IsActive)
					{
						return true;
					}
				}
				return false;
			}
		}

		public override IList<T> RecreateActiveFilters()
		{
			Filters.Clear();
			foreach (FilterPresenter<T> filter in RegisteredFilters)
			{
				if (filter.IsActive)
				{
					var filters = filter.Filters;
					if (filters != null && filters.Length > 0)
					{
						foreach (var f in filters)
						{
							Filters.Add(f);
						}
					}
				}
			}
			WasChanged = false;
			return Filters;
		}

		void InitCommands()
		{
			AddCommand = new DelegateCommand<object>(
				arg =>
				{
					if (arg is FilterPresenter<T>)
					{
						var filter = (FilterPresenter<T>)arg;
						if (RegisteredFilters.Contains(filter))
						{
							filter.Reset();
							filter.IsActive = true;
							RefreshFiltersCollection();
							//NotUsedFilters.Clear();
							OnPropertyChanged("HasNotActiveFilters");
						}
						WasChanged = true;
					}
				});
			RefreshCommand = new DelegateCommand<object>((o) => RefreshFiltersCollection());
		}

		public void RefreshFiltersCollection()
		{
			NotUsedFilters.Clear();
			foreach (var filter in RegisteredFilters)
			{
				if (!filter.IsActive)
				{
					NotUsedFilters.Add(filter);
				}
			}
			OnPropertyChanged("HasActiveFilters");
			OnPropertyChanged("HasNotActiveFilters");
		}

		public FiltersManagerPresenter()
			: base()
		{
			RegisteredFilters = new ObservableCollection<FilterPresenter<T>>();
			NotUsedFilters = new ObservableCollection<FilterPresenter<T>>();
			InitCommands();
		}

		protected void RegisteringFilters(params FilterPresenter<T>[] filters)
		{
			if (filters != null)
			{
				foreach (var filter in filters)
				{
					filter.Owner = this;
					RegisteredFilters.Add(filter);
				}
			}
			RefreshFiltersCollection();
		}

		public void FiltersChanged()
		{
			WasChanged = true;
		}
	}
}
