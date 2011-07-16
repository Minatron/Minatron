namespace SettingsModule.Views
{
    public partial class LangSettingView
    {
        public LangSettingView(Presenters.LangSettingPresenter presenter)
        {
            InitializeComponent();
            DataContext = presenter;
        }
    }
}
