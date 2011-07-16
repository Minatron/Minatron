namespace StorageModule.Views
{    
    public partial class StorageSettingsView
    {
        public StorageSettingsView(Presenters.StorageSettingsPresenter presenter)
        {
            InitializeComponent();
            DataContext = presenter;
        }
    }
}
