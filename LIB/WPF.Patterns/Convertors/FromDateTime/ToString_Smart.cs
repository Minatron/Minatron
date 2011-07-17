using System;

namespace Band.WPF.Convertors
{
    
    public class DateTimeToString_SmartConvertor : DateTimeCovertor
    {
        public DateTimeToString_SmartConvertor(From from) : base(from) { }
        public DateTimeToString_SmartConvertor(From from, To to) : base(from,to) { }


        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TimeSpan span = (DateTime)value - FromNow;
            
            if ((-1 < span.Days) && (span.Days < 1))   return ToDateTime((DateTime)value).ToString(TimeFormat(parameter));
			else return ToDateTime((DateTime)value).ToString(FullFormat(parameter));
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
