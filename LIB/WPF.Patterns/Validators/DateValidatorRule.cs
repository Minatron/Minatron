using System;
using System.Globalization;
using System.Windows.Controls;

namespace Band.WPF.Validators
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
