
namespace WPF.Patterns.ViewModel
{
    public interface IUpdatebleViewModel
    {
        void ClearModel();
        void UpdateModel();
        void UpdateModel(object model);
    }
}
