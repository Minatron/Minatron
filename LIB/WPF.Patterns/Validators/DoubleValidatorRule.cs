using System;
using System.Globalization;
using System.Windows.Controls;
using Band.WPF.Localization;

namespace Band.WPF.Validators
{
    public class DoubleValidatorRule:ValidationRule
    {
        public double Min { get; set; }

        public double Max { get; set; }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double res;
            var calInfo = CultureInfo.InvariantCulture;
            if(Double.TryParse((string)value,NumberStyles.Any,calInfo,out res))
            {
                if(res<=Max&&res>=Min)
                {
                    return new ValidationResult(true, null);
                }


                return new ValidationResult(false, Lang.GetTitle("ValidateRules/RangeMessage")
                    + this.Min + " - " + this.Max + ".");
            }
            return new ValidationResult(false, Lang.GetTitle("ValidateRules/DoubleMessage"));
        }
    }
}
