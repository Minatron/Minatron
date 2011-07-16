using System;
using System.ComponentModel;
using ImagesStoreSystem.DBProvider.Core;

namespace StorageModule.Model
{
	public class DictionaryItemProxy : IDeletable
	{
		bool _isDeleted = false;
		string _title;

		public bool IsDeleted
		{
			get
			{
				return _isDeleted;
			}
			set
			{
				_isDeleted = value;
				OnPropertyChanged("IsDeleted");
			}
		}

		public DictionaryBase Item { get; protected set; }

		public string Title
		{
			get { return _title; }
			set
			{
				if (string.IsNullOrWhiteSpace(value)) _title = "";
				else _title = value.Trim();
				OnPropertyChanged("Title");
			}
		}

		public virtual bool Save(DictionaryBase objItem)
		{
			if (!IsDeleted)
			{
				Item = objItem;
				Item.Title = Title;
				return true;
			}
			return false;
		}

		public DictionaryItemProxy(DictionaryBase item)
		{
			if (item == null) throw new ArgumentNullException("item");
			Item = item;
			Reset();
		}

		public virtual void Reset()
		{
			IsDeleted = false;
			Title = Item.Title;
		}

        public virtual void Clear()
        {
            IsDeleted = false;
            Title = "";
        }


		#region INotifyPropertyChanged

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null) return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion INotifyPropertyChanged

		public static DictionaryItemProxy Create(DictionaryBase item)
		{
			DictionaryItemProxy proxy;
			if (item is NumberedDictionaryBase)
			{
				proxy = new NumberedDictionaryItemProxy(item as NumberedDictionaryBase);
			}
			else
			{
				proxy = new DictionaryItemProxy(item);
			}
			return proxy;
		}
	}

	public class NumberedDictionaryItemProxy : DictionaryItemProxy
	{
		public long CatalogNumber { get; set; }

		public NumberedDictionaryItemProxy(NumberedDictionaryBase item) : base(item) { }

		public override bool Save(DictionaryBase objItem)
		{
			if (base.Save(objItem))
			{
				(Item as NumberedDictionaryBase).CatalogNumber = CatalogNumber;
				return true;
			}
			return false;
		}

		public override void Reset()
		{
			base.Reset();
			CatalogNumber = (Item as NumberedDictionaryBase).CatalogNumber;
            OnPropertyChanged("CatalogNumber");
		}

        public override void Clear()
        {
            base.Clear();
            CatalogNumber = 0;
            OnPropertyChanged("CatalogNumber");
        }
	}
}
