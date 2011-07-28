using System.Globalization;
using System.Windows.Controls;
using Band.WPF.Localization;

namespace Band.WPF.Validators
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
