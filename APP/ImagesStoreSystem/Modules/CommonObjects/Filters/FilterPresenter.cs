using System.ComponentModel;
using System.Windows.Input;
using ImagesStoreSystem.DBProvider.Core;
using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Events;
using WPF.Patterns.Commands;

namespace CommonObjects
{
	public abstract class FilterPresenter<T> : INotifyPropertyChanged where T : IStorageFilter
	{
		bool _isActive = false;
		public ICollectionFiltersManager<T> Owner { get; set; }

		protected readonly IEventAggregator _eventAggregator;
		public abstract string Name { get; }
		public abstract T[] Filters { get; }
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

		public FilterPresenter(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<LangChangedEvent>().Subscribe(obj => OnPropertyChanged("Name"));
			RemoveCommand = new DelegateCommand(() => IsActive = false);
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
