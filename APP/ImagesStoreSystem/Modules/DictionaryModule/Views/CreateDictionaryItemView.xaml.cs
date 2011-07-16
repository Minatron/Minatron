using DictionaryModule.Presenters;

namespace DictionaryModule.Views
{
	public partial class CreateDictionaryItemView 
	{
		public CreateDictionaryItemView(CreateDictionaryItemPresenter presenter)
		{
			InitializeComponent();
			DataContext = presenter;
		}
	}
}
