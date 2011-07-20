namespace Band.Module.WeighData.Views
{
    public partial class WeighDataView 
    {
        public WeighDataView(Presenters.WeighDataPresenter presenter)
        {
            InitializeComponent();
            DataContext = presenter;
        }
    }
}
