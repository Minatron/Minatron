using System.Collections.Generic;
using System.Windows.Input;
using ImagesStoreSystem.DBProvider.Core;

namespace CommonObjects
{
	public interface ICollectionFiltersManager<T> : IFiltersManager<T> where T : IStorageFilter
	{
		ICommand AddCommand { get; }
		void RefreshFiltersCollection();
		void FiltersChanged();
	}

	public interface IFiltersManager<T> where T : IStorageFilter
	{
		bool WasChanged { get; }
		IList<T> RecreateActiveFilters();
		IList<T> Filters { get; }
	}
}
