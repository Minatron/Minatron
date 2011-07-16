using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using LocalizationLibrary;

namespace ValidatorRules
{
    public class IntValidatorRule : ValidationRule
    {
        public int Min { get; set; }

        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int res;
            if(Int32.TryParse((string)value,out res))
            {
                if (res <= Max && res >= Min)
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, Lang.GetTitle("ValidateRules/RangeMessage")
                    + this.Min + " - " + this.Max + ".");
            }
            return new ValidationResult(false, Lang.GetTitle("ValidateRules/WrongPortMessage"));
        }
    }
}
