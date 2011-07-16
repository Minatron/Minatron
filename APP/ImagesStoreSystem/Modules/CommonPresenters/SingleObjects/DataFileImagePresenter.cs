using System;
using System.IO;
using System.Windows.Media.Imaging;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Services;

namespace CommonPresenters
{
	public class DataFileImagePresenter : PreviewPresenter
	{
		StorageService m_storage;
		DataFile m_file;

		void RefreshWith(Stream stream)
		{
			BitmapImage img = new BitmapImage();
			try
			{
				img.BeginInit();
				img.StreamSource = stream;
				img.EndInit();
			}
			catch
			{
				img = null;
			}
			ImageSource = img;
		}

		void RefreshImage()
		{
			m_storage.GetFileManager().StreamAction(m_file, RefreshWith);
		}

		public DataFileImagePresenter(DataFile file, StorageService storage)
			: base()
		{
			if (file == null) throw new NullReferenceException("file");
			if (storage == null) throw new NullReferenceException("storage");

			m_file = file;
			m_storage = storage;

			RefreshImage();
		}
	}
}
