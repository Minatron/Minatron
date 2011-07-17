using System;
using System.Windows.Data;

namespace Band.WPF.Convertors
{
    [ValueConversion(typeof(DateTimeCovertor), typeof(string))]
    public abstract class DateTimeCovertor : IValueConverter
    {

        public enum To
        {
            UTC,
            Local
        }
        public enum From
        {
            UTC,
            Local
        }

        public static string DefaultTimeFormat = @"HH:mm:ss";
        public static string DefaultDateFormat = @"dd.MM.yyyy";
        public static string DefaultFullFormat = DefaultDateFormat + @" " + DefaultTimeFormat;

        To? to;
        From? from;
        public DateTimeCovertor()
        {
            to = null;
            from = null;
        }
        public DateTimeCovertor(To t)
        {
            to = t;
            from = null;
        }
        public DateTimeCovertor(From f)
        {
            to = null;
            from = f;
        }
        public DateTimeCovertor(From f, To t)
        {
            to = t;
            from = f;
        }
        protected DateTime ToDateTime(DateTime value)
        {
            if (!to.HasValue)
            {
                return (value);
            }
            else if (to.Value == To.Local)
            {
                return (value).ToLocalTime();
            }
            else if (to.Value == To.UTC)
            {
                return (value).ToUniversalTime();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

		protected DateTime FromDateTime(DateTime value)
		{
			if (!to.HasValue)
            {
                return (value);
            }
            else if (to.Value == To.Local)
            {
                return (value).ToUniversalTime();
            }
            else if (to.Value == To.UTC)
            {
                return (value).ToLocalTime();
            }
            else
            {
                throw new NotImplementedException();
            }
		}

        protected DateTime FromNow
        {
            get
            {
                if (!from.HasValue)
                {
                    throw new InvalidOperationException();
                }
                else if (from.Value == From.Local)
                {
                    return DateTime.Now;
                }
                else if (from.Value == From.UTC)
                {
                    return DateTime.UtcNow;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

		


        protected string FullFormat(object parameter)
        {
            string format = (string)parameter;
            if (string.IsNullOrEmpty(format)) format = DefaultFullFormat;
            return format;
        }
        protected string TimeFormat(object parameter)
        {
            string format = (string)parameter;
            if (string.IsNullOrEmpty(format)) format = DefaultTimeFormat;
            return format;
        }
        protected string DateFormat(object parameter)
        {
            string format = (string)parameter;
            if (string.IsNullOrEmpty(format)) format = DefaultDateFormat;
            return format;
        }


        public abstract object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);



    }
}
