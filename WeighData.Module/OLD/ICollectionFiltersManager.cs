using System.Windows.Input;

namespace Band.OLD
{
    public interface ICollectionFiltersManager : IFiltersManager 
    {
        ICommand AddCommand { get; }
        void RefreshFiltersCollection();
        void FiltersChanged();
    }
}