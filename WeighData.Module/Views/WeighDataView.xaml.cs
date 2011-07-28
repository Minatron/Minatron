using Band.Module.WeighData.Presenters;

namespace Band.Module.WeighDataModule.Views
{
    public partial class WeighDataView 
    {
        public WeighDataView(WeighDataPresenter presenter)
        {
            InitializeComponent();
            DataContext = presenter;
        }
    }
}
