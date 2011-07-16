using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ValidatorRules
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
