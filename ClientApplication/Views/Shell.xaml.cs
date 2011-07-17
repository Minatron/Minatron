namespace Band.Client.App
{
    public partial class Shell 
    {
        public Shell(Presenters.ShellPresenter presenter)
        {
            InitializeComponent();
            DataContext = presenter;
        }
    }
}
