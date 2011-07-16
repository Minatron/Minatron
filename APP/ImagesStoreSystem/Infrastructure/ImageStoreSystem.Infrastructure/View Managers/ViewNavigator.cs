using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace ImageStoreSystem.Infrastructure
{
	public class ViewNavigator : INotifyPropertyChanged
	{
		Stack<RegionContentPair> m_views = new Stack<RegionContentPair>();
		RegionContentPair _currentView = null;
		public string LastViewName
		{
			get
			{
				if (_currentView != null)
				{
					return _currentView.ViewName ?? @"BackButton";
				}
				return @"BackButton";
			}
		}


		void ActivatePreviousView()
		{
			if (m_views.Count > 0)
			{
				_currentView = m_views.Pop();
				_currentView.ActivateView();
			}

			OnPropertyChanged("HasPrewViews");
			OnPropertyChanged("CanActivatePrevView");
			OnPropertyChanged("LastViewName");
		}

		public ICommand ShowPreviousContent { get; protected set; }

		public bool HasPrewViews
		{
			get
			{
				return m_views.Count > 1;
			}
		}

		public bool CanActivatePrevView
		{
			get
			{
				return HasPrewViews;
			}
		}

		public ViewNavigator(IEventAggregator eventAgregator)
		{
			ShowPreviousContent = new DelegateCommand<object>(
				arg =>
				{
					ActivatePreviousView();
				},
				arg => HasPrewViews);

			eventAgregator.GetEvent<ActivateEvent>().Subscribe(
				(arg) =>
				{
					if (arg == null || arg.RegionContentPair == null) return;
					if (IsSameContent(arg.RegionContentPair)) return;

					if (arg.IsNewSequence)
					{
						_currentView = null;
						m_views.Clear();
					}
					if (_currentView != null)
					{
						_currentView.DeactivateView();
						m_views.Push(_currentView);
					}
					_currentView = arg.RegionContentPair;
					_currentView.ActivateView();

					OnPropertyChanged("HasPrewViews");
					OnPropertyChanged("CanActivatePrevView");
					OnPropertyChanged("LastViewName");
				});
			eventAgregator.GetEvent<ShowPreviousView>().Subscribe(arg => ActivatePreviousView(), true);
		}

		bool IsSameContent(RegionContentPair pair)
		{
			return pair != null && _currentView != null && _currentView.Content.GetType().Equals(pair.Content.GetType());
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
