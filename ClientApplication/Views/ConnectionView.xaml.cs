
namespace Band.Client.App.Views
{
    public partial class ConnectionView 
    {
        public ConnectionView(Presenters.ConnectionPresenter presenter)
        {
            InitializeComponent();
            DataContext = presenter;
        }
    }
}
