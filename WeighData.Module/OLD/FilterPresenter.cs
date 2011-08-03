using System.ComponentModel;
using System.Windows.Input;
using Band.Storage;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace Band.OLD
{
	public abstract class FilterPresenter : INotifyPropertyChanged
	{
		bool _isActive = false;
        public ICollectionFiltersManager Owner { get; set; }

		protected readonly IEventAggregator _eventAggregator;
		public abstract string Name { get; }
        public abstract IStorageFilter[] Filters { get; }
		public bool IsActive 
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
				if (Owner != null)
				{
					Owner.RefreshFiltersCollection();
				}
				OnPropertyChanged("IsActive");
			}
		}

		public abstract void Reset();

		public ICommand RemoveCommand{get; protected set;}

		public FilterPresenter()
		{
			RemoveCommand = new DelegateCommand<object>((o) => IsActive = false);
		}

		#region INotifyPropertyChanged

		protected void FilterChanged()
		{
			if (Owner != null) Owner.FiltersChanged();
		}

		protected void OnPropertyChanged(string propertyName)
		{
			if (!propertyName.Equals("Name")) FilterChanged();
			if (PropertyChanged == null) return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion INotifyPropertyChanged
	}
}
