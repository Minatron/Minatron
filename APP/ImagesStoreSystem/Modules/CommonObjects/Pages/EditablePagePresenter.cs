using System.Windows.Input;
using ImagesStoreSystem.DBProvider.Core;
using Microsoft.Practices.Composite.Events;
using WPF.Patterns.Commands;

namespace CommonObjects
{
	public abstract class EditablePagePresenter<T> : PagePresenter<T> where T : UpdatableObject
	{
		public ICommand ViewItemCommand { get; protected set; }
		public ICommand AddItemCommand { get; protected set; }

		void InitCommands()
		{
			ViewItemCommand = new DelegateCommand<object>((object obj) => OnViewCommandExecute(obj as T));
			AddItemCommand = new DelegateCommand(() => OnAddCommandExecute());
		}

		protected abstract void OnViewCommandExecute(T obj);
		protected abstract void OnAddCommandExecute();

		public EditablePagePresenter(int capacity, IEventAggregator eventAggregator)
			: base(eventAggregator, capacity)
		{
			InitCommands();
		}
	}
}
