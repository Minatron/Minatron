using ReceivePlanModule.Presenters;

namespace ReceivePlanModule.View
{
	public partial class ReceivePlanPageView
	{
		public ReceivePlanPageView(ReceivePlanPagePresenter presenter)
		{
			InitializeComponent();
			DataContext = presenter;
		}
	}
}
