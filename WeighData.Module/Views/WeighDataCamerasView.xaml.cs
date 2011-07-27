namespace Band.Module.WeighData.Views
{
    public partial class WeighDataCamerasView 
    {
        public WeighDataCamerasView(Presenters.WeighDataCamerasPresenter presenter)
        {
            InitializeComponent();
            DataContext = presenter;
        }
    }
}
