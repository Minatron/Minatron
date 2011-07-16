namespace ImagesStoreSystem
{
    
    public partial class Shell
    {
        public Shell(ShellPresenter presenter)
        {
            InitializeComponent();
            DataContext = presenter;
        }
    }
}
