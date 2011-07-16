using System.Collections.ObjectModel;
using System.Windows.Input;
using ImagesStoreSystem.DBProvider.Core;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace CommonObjects
{
	public class FieldsManagerBase<T> : Presenter  where T : UpdatableWithPacketObject
	{
		public ObservableCollection<IFieldPresenterBase<T>> Fields { get; protected set; }
		public ObservableCollection<IFieldPresenterBase<T>> UnUsedFields { get; protected set; }

		public ICommand RemoveFieldCommand { get; protected set; }
		public ICommand AddCommand { get; protected set; }

		public bool IsAllCorrect
		{
			get
			{
				foreach (var field in Fields)
				{
					if (!field.IsCorrect) return false;
				}
				return true;
			}
		}

		public virtual void ApplyTo(T obj)
		{
			foreach (var field in Fields)
			{
				field.ApplyTo(obj);
			}
		}

		public FieldsManagerBase(IEventAggregator eventAggregator)
			: base(eventAggregator) 
		{
			Fields = new ObservableCollection<IFieldPresenterBase<T>>();
			UnUsedFields = new ObservableCollection<IFieldPresenterBase<T>>();

			RemoveFieldCommand = new DelegateCommand<IFieldPresenterBase<T>>(field => SetValueToField(field, false));

			AddCommand = new DelegateCommand<IFieldPresenterBase<T>>(field => SetValueToField(field, true));
		}

		protected void RegisterFields(params IFieldPresenterBase<T>[] fields)
		{
			if (fields != null)
			{
				foreach (var field in fields)
				{
					if (!Fields.Contains(field))
					{
						Fields.Add(field);
					}
				}
			}
		}

		void SetValueToField(IFieldPresenterBase<T> field, bool hasValue)
		{
			if (field != null && Fields.Contains(field))
			{
				field.HasValue = hasValue;
				field.Reset();
				if (field.CanRemove)
				{
					if (hasValue)
					{
						UnUsedFields.Remove(field);
					}
					else
					{
						UnUsedFields.Add(field);
					}
					UnUsedFields.Sort(ComparerForFields);
				}
			}
		}

		public void InitFor(T obj)
		{
			UnUsedFields.Clear();
			foreach (var field in Fields)
			{
				field.InitFor(obj);
				if (!field.HasValue)
				{
					UnUsedFields.Add(field);
				}
			}
			UnUsedFields.Sort(ComparerForFields);
		}

		int ComparerForFields(IFieldPresenterBase<T> field1, IFieldPresenterBase<T> field2)
		{
			if (field1 == field2) return 0;
			if (field1 == null) return -1;
			if (field2 == null) return 1;
			if (field1.CanRemove == field2.CanRemove) return string.Compare(field1.Title, field2.Title);
			if (!field1.CanRemove) return 1;
			if (!field2.CanRemove) return -1;
			return 0;
		}
	}
}
