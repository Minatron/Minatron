using System;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Services;

namespace CommonPresenters
{
	public class ImagePresenter : Presenter
	{
		Image m_image;
		StorageService m_storage;
        IDataAccessObjects m_dao;
		DataFileImagePresenter m_preview;

		public String Title
		{
			get
			{
				return m_image.Data.Title;
			}
		}

		public DateTime CreateTime
		{
			get
			{
				return m_image.Time;
			}
		}

		public bool HasPreview
		{
			get
			{
				return m_preview.HasPreview;
			}
		}

		public DataFileImagePresenter Preview
		{
			get
			{
				return m_preview;
			}
		}

		void RefreshPreview()
		{
			m_preview = null;
			var packet= m_image.Data;
			//m_storage.Update(packet);

			var collection = packet.Files;
			foreach (DataFile file in collection)
			{
				if (file.Title.StartsWith(Constants.previewFileName))
				{
					m_preview = new DataFileImagePresenter(file, m_storage);
				}
			}
		}

		public ImagePresenter(StorageService storage, long imageId)
		{
			if (storage == null) throw new NullReferenceException("storage");

			m_dao = storage.GetDAO();
			m_storage = storage;

			m_image = m_dao.ReadByID<Image>(imageId);
			RefreshPreview();
		}

		public static explicit operator Image(ImagePresenter presenter)
		{
			return presenter.m_image;
		}
	}
}
