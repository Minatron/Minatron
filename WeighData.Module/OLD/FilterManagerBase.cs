using System.Collections.Generic;
using System.ComponentModel;
using Band.Storage;

namespace Band.OLD
{
    public abstract class FilterManagerBase : IFiltersManager, INotifyPropertyChanged 
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

        public IList<IStorageFilter> Filters { get; protected set; }

        public FilterManagerBase()
        {
            Filters = new List<IStorageFilter>();
        }

        public abstract IList<IStorageFilter> RecreateActiveFilters();

        #region INotifyPropertyChanged

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INotifyPropertyChanged
    }
}
