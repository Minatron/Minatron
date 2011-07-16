using DictionaryModule.Presenters;

namespace DictionaryModule.Views
{
	public partial class DictionaryProxyEditorView
	{
		public DictionaryProxyEditorView(DictionaryProxyPresenter presenter)
		{
			InitializeComponent();
			DataContext = presenter;
		}
	}
}
