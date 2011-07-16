using System;
using System.IO;
using ImagesStoreSystem.DBProvider.Core;
using System.ComponentModel;

namespace StorageModule.Model
{
	public class NewFileProxy : INewFileInfo, IDeletable
	{
		FileInfo _file;
		bool _isDeleted = false;

        public string SourcePath
		{
			get
			{
                return _file.FullName;
			}
		}

		public FileInfoType TypeInfo { get; set; }

		public long Size
		{
			get
			{
				return _file.Length;
			}
		}

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime Time
		{
			get
			{
				return _file.LastWriteTimeUtc;
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

		public NewFileProxy(FileInfo file)
		{
			if (file == null) throw new ArgumentNullException("file");
			_file = file;
			Reset();
		}

		public void Reset()
		{
			Name = _file.Name;
			TypeInfo = FileInfoType.Default;
			Description = null;
			IsDeleted = false;

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
