using WeightDataTest.Module.Presenters;

namespace WeightDataTest.Module.Views
{
    public partial class WeighDataView 
    {
        public WeighDataView(WeighDataTestPresenter testPresenter)
        {
            InitializeComponent();
            DataContext = testPresenter;
        }
    }
}
