using System;

namespace WPF.Patterns.Convertors
{

    public class DateTimeToStringConvertor : DateTimeCovertor
    {
        public DateTimeToStringConvertor():base() {}
        public DateTimeToStringConvertor(To to):base(to){}

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			if (value is DateTime)
			{
				return ToDateTime((DateTime)value).ToString(FullFormat(parameter));
			}
			return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			string dateTimeString = value as string;
			if (dateTimeString == null) throw new InvalidOperationException();

			DateTime local;
			if (!(DateTime.TryParse(dateTimeString, out local))) return null;// throw new InvalidOperationException();

			return FromDateTime(local);
        }
    }
}
