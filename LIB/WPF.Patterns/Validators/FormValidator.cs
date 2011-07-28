using System.Windows;
using System.Windows.Controls;

namespace Band.WPF.Validators
{
    public class FormValidator
    {
        public static bool FormHasErrors(DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                var  element = child as DependencyObject;
                if(element==null) continue;
                if(Validation.GetHasError(element))
                {
                    return true;
                }
                if (FormHasErrors(element))
                {
                    return true;
                }
            }
            return false;
        }        
    }
}
