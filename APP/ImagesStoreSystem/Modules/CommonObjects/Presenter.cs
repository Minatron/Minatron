using System;
using System.ComponentModel;
using ImagesStoreSystem.DBProvider.Core;
using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Events;

namespace CommonObjects
{
	public abstract class Presenter : INotifyPropertyChanged
	{
		bool _hasProblems = false;
		string _errorMessage = null;
		protected readonly IEventAggregator _eventAggregator;

		public String ErrorMessage
		{

			get
			{
				return _errorMessage;
			}
			protected set
			{
				_errorMessage = value;
				OnPropertyChanged("ErrorMessage");
			}
		}

		public bool HasProblems
		{
			get
			{
				return _hasProblems;
			}
			protected set
			{
				_hasProblems = value;
				OnPropertyChanged("HasProblems");
			}
		}

		protected void ActionWithConnectException(Action action)
		{
			try
			{
				action();
				HasProblems = false;
			}
			catch (ConnectException)
			{
				HasProblems = true;
				if (_eventAggregator != null)
				{
					_eventAggregator.GetEvent<DisconnectEvent>().Publish(null);
				}
			}
		}

		protected void ActionWithOperationException(Action action)
		{
			try
			{
				action();
				HasProblems = false;
			}
			catch (OperationException)
			{
				HasProblems = true;
			}
		}

		public void ActionWithException(Action action)
		{
			try
			{
				action();
				HasProblems = false;
			}
			catch (ConnectException)
			{
				HasProblems = true;
				if (_eventAggregator != null)
				{
					_eventAggregator.GetEvent<DisconnectEvent>().Publish(null);
				}
			}
			catch (OperationException)
			{
				HasProblems = true;
			}
		}

		public Presenter(IEventAggregator eventAggregator)
		{
			if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
			_eventAggregator = eventAggregator;
		}

		#region INotifyPropertyChanged

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null) return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
