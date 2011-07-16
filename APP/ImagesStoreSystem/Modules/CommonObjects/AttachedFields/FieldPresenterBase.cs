using System.ComponentModel;
using ImagesStoreSystem.DBProvider.Core;
using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Events;

namespace CommonObjects
{
	public abstract class FieldPresenterBase<T> : Presenter, IFieldPresenterBase<T>, INotifyPropertyChanged where T : UpdatableWithPacketObject
	{
		bool _hasValue;

		public bool HasValue
		{
			get
			{
				return _hasValue;
			}
			set
			{
				_hasValue = value;
				OnPropertyChanged("HasValue");
			}
		}

		public FieldPresenterBase(IEventAggregator eventAggregator) :base (eventAggregator)
		{
			_eventAggregator.GetEvent<LangChangedEvent>().Subscribe(arg => OnPropertyChanged("Title"));
		}

		public virtual bool CanRemove
		{
			get
			{
				return true;
			}
		}

		public virtual void Reset() { }

		public abstract void InitFor(T obj);

		public abstract void ApplyTo(T obj);

		public abstract string Title { get; }

		public abstract bool IsCorrect { get; }
	}
}
