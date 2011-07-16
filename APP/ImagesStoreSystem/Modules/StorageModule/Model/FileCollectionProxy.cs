using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ImagesStoreSystem.DBProvider.Core;
using System.IO;

namespace StorageModule.Model
{
	public class FileCollectionProxy 
	{
		public ObservableCollection<IFileInfo> Collection { get; protected set; }

		public FileCollectionProxy()
		{
			Collection = new ObservableCollection<IFileInfo>();
		}

		public void Clear()
		{
			Collection.Clear();
		}

		public void Reset(IList<DataFile> files)
		{
			Clear();
			foreach (var file in files)
			{
				Collection.Add(new EditFileProxy(file));
			}
		}

		public void AddNewFiles(IEnumerable<String> fileNames)
		{
			List<FileInfo> files = new List<FileInfo>();
			foreach (var file in fileNames)
			{
				files.Add(new FileInfo(file));
			}
			AddNewFiles(files);
		}

		public void AddNewFiles(IEnumerable<FileInfo> files)
		{
			foreach (var file in files)
			{
				Collection.Add(new NewFileProxy(file));
			}
		}

		public IList<INewFileInfo> GetNewFiles()
		{
			List<INewFileInfo> newFiles = new List<INewFileInfo>();
			foreach (var file in Collection)
			{
				var newFile = file as NewFileProxy;
				if (newFile != null && !newFile.IsDeleted)
				{
					newFiles.Add(newFile);
				}
			}
			return newFiles;
		}

		public IList<EditFileProxy> GetDeletedFiles()
		{
			var res = new List<EditFileProxy>();
			foreach (var file in Collection.OfType<EditFileProxy>())
			{
				if (file.IsDeleted)
				{
					res.Add(file);
				}
			}
			return res;
		}
	}
}
