
namespace Band.Client.App.Views
{
    public partial class LostConnectView 
    {
        public LostConnectView(Presenters.ConnectionPresenter presenter)
        {
            InitializeComponent();
            DataContext = presenter;
        }
    }
}
