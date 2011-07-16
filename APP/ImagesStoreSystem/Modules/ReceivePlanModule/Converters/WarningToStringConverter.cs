using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using CommonObjects;
using LocalizationLibrary;

namespace ReceivePlanModule.Converters
{
	[ValueConversion(typeof(PageWarning), typeof(String))]
	class WarningToStringConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is PageWarning)
			{
				PageWarning warning = (PageWarning)value;
				switch (warning)
				{
					case PageWarning.NoObjectsInDB:
                        return Lang.GetTitle("Modules/ReceivePlan/PageView/NoPlansInDb");

					case PageWarning.NoObjectsForThisFilter:
                        return Lang.GetTitle("Modules/ReceivePlan/PageView/SearchNoResults");
					default:
						return null;
				}
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
