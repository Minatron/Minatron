using System;
using ImagesStoreSystem.DBProvider.Core;

namespace CommonPresenters
{
	public class ImageLevelPresenter : Presenter
	{
		ImageLevel m_level;

		public long ID
		{
			get
			{
				return m_level.Id;
			}
		}

		public String Title
		{
			get
			{
				return m_level.Title;
			}
			set
			{
				m_level.Title = value;
				OnPropertyChanged("Title");
			}
		}

		public ImageLevelPresenter(ImageLevel level)
		{
			if (level == null) throw new NullReferenceException("level");
			m_level = level;
		}

		public static explicit operator ImageLevel(ImageLevelPresenter presenter)
		{
			return presenter.m_level;
		}
	}
}
