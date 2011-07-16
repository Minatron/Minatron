using StorageModule.Presenters;

namespace StorageModule.Views
{
	public partial class LostConnectView 
	{
		public LostConnectView(StorageSettingsPresenter presenter)
		{
			InitializeComponent();
			DataContext = presenter;
		}
	}
}
