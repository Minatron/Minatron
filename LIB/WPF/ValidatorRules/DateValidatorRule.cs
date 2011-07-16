using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ValidatorRules
{
    public class DateValidatorRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {            
            IFormatProvider culture = new CultureInfo("ru-RU", true);
            try
            {
                DateTime.ParseExact((string) value, "dd.MM.yyyy HH:mm:ss", culture);
                return new ValidationResult(true, null);
            }
            catch (Exception)
            {

                return new ValidationResult(false, null);  
            }
            
                        
                      
        }
    }
}
