using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using LocalizationLibrary;

namespace ValidatorRules
{
    public class IPValidatorRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            System.Net.IPAddress address;
            if (System.Net.IPAddress.TryParse((string)value, out address))
            {
                return new ValidationResult(true, null);    
            }
            return new ValidationResult(false, Lang.GetTitle("ValidateRules/WrongIPMessage"));
        }
    }
}
