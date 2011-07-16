using System.Windows.Media;

namespace CommonPresenters
{
	public class PreviewPresenter : Presenter
	{
		ImageSource m_imageSource = null;

		public bool HasPreview
		{
			get
			{
				return m_imageSource == null;
			}
		}

		public ImageSource ImageSource
		{
			get
			{
				return m_imageSource;
			}
			protected set
			{
				m_imageSource = value;
				OnPropertyChanged("ImageSource");
				OnPropertyChanged("HasPreview");
			}
		}
	}
}
