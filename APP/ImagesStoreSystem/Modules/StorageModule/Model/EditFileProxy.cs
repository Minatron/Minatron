using System;
using System.ComponentModel;
using ImagesStoreSystem.DBProvider.Core;

namespace StorageModule.Model
{
	public class EditFileProxy : IFileInfo, IDeletable
	{
		bool _isDeleted = false;

		internal DataFile File { get; private set; }

		public FileInfoType TypeInfo { get; set; }

		public long Size
		{
			get
			{
				return File.Size;
			}
		}

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime Time
		{
			get
			{
				return File.Time;
			}
		}

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

		public EditFileProxy(DataFile file)
		{
			if (file == null) throw new ArgumentNullException("file");

			File = file;
			Reset();
		}

		public void Save(DataFile dstFile)
		{
			if (!IsDeleted)
			{
				File = dstFile;
				File.Name = Name;
				File.Description = Description;
				File.TypeInfo = TypeInfo;
			}
		}

		public void Reset()
		{
			IsDeleted = false;
			Name = File.Name;
			Description = File.Description;
			TypeInfo = File.TypeInfo;

			OnPropertyChanged("Name");
			OnPropertyChanged("TypeInfo");
			OnPropertyChanged("Description");
		}

		#region INotifyPropertyChanged

		void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null) return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion INotifyPropertyChanged
	}
}
