namespace SettingsModule.Views
{
    public partial class SettingsContentView 
    {
        public SettingsContentView(Presenters.SettingsContentPresenter presenter)
        {
            InitializeComponent();
            DataContext = presenter;
        }
    }
}
