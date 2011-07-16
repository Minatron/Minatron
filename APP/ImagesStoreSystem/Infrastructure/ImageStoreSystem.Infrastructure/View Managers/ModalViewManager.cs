using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using Microsoft.Practices.Composite.Events;
using System.Windows.Threading;
using System.Threading;
using System.Windows;

namespace ImageStoreSystem.Infrastructure
{
	public enum ModalViewType
	{
		Content,
		Center
	}

	public class ModalViewManager : INotifyPropertyChanged
	{
		struct Content
		{
			public ModalViewType Type;
			public Control Control;
		}

		Control _content;
		Stack<Content> _stack = new Stack<Content>();

		public bool HasContent
		{
			get
			{
				return _stack.Count > 0;
			}
		}

		public Control ContentControl
		{
			get
			{
				if (HasContent)
				{
					var content = _stack.Peek();
					if (content.Type == ModalViewType.Content)
					{
						return content.Control;
					}
				}
				return null;
			}
		}

		public Control CenterControl
		{
			get
			{
				if (HasContent)
				{
					var content = _stack.Peek();
					if (content.Type == ModalViewType.Center)
					{
						return content.Control;
					}
				}
				return null;
			}
		}


		public ModalViewManager(IEventAggregator eventAgregator)
		{
			eventAgregator.GetEvent<DeactivateModalViewEvent>().Subscribe(Hide, true);
			eventAgregator.GetEvent<ActivateModalViewEvent>().Subscribe(c => Show(c, ModalViewType.Center), true);
		}

		public void Hide(Control control=null)
		{
			if (_stack.Count > 0)
			{
				_stack.Pop();
				OnPropertyChanged();
			}
		}

		public void Show(Control control, ModalViewType type = ModalViewType.Content)
		{
			if (control != null)
			{
				_stack.Push(new Content() { Control = control, Type = type });
				OnPropertyChanged();
			}
		}

		void OnPropertyChanged()
		{
			OnPropertyChanged("CenterControl");
			OnPropertyChanged("ContentControl");
			OnPropertyChanged("HasContent");
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
