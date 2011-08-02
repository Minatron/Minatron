using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Band.Storage;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace Band.OLD
{
    public class FiltersManagerPresenter : FilterManagerBase, ICollectionFiltersManager 
	{
        public ObservableCollection<FilterPresenter> RegisteredFilters { get; protected set; }
        public ObservableCollection<FilterPresenter> NotUsedFilters { get; protected set; }

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

        public override IList<IStorageFilter> RecreateActiveFilters()
		{
			Filters.Clear();
            foreach (FilterPresenter filter in RegisteredFilters)
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
                    if (arg is FilterPresenter)
					{
                        var filter = (FilterPresenter)arg;
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
            RegisteredFilters = new ObservableCollection<FilterPresenter>();
            NotUsedFilters = new ObservableCollection<FilterPresenter>();
			InitCommands();

            RegisteringFilters(
                new CourseFilterPresenter(),
                new WeightTimeFilterPresenter(),
                new WeightFilterPresenter()
                );
		}

        protected void RegisteringFilters(params FilterPresenter[] filters)
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
