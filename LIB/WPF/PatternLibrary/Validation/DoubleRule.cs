using System.Windows.Controls;

namespace WPF.Patterns.Convertors
{
	public class DoubleRule : ValidationRule
	{
		public double Max { get; set; }
		public double Min { get; set; }

		public DoubleRule()
		{
			Min = double.MinValue;
			Max = double.MaxValue;
		}

		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			double doubleValue = 0;
			try
			{
				if (!string.IsNullOrWhiteSpace((string)value))
				{
					doubleValue = double.Parse((string)value, cultureInfo);
				}
			}
			catch
			{
				return new ValidationResult(false, "not correct value");
			}
			if (doubleValue < Min || doubleValue > Max)
			{
				return new ValidationResult(false, string.Format("value is not in range [{0}..{1}]", Min, Max));
			}
			else
			{
				return new ValidationResult(true, null);
			}
		}
	}
}
