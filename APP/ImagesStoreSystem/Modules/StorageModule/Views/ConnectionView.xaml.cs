using StorageModule.Presenters;

namespace StorageModule.Views
{
	/// <summary>
	/// Interaction logic for ConnectionView.xaml
	/// </summary>
	public partial class ConnectionView
	{
		public ConnectionView(StorageSettingsPresenter presenter)
		{
			InitializeComponent();
			DataContext = presenter;
		}
	}
}
