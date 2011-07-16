using System.Windows.Data;

namespace WPF.Patterns.Convertors
{

    public static class UTC
    {
        public new static readonly IValueConverter ToString = new DateTimeToStringConvertor();
        public static readonly IValueConverter ToString_Smart = new DateTimeToString_SmartConvertor(DateTimeCovertor.From.UTC, DateTimeCovertor.To.UTC);
    }
    public static class ToLocalTime
    {
        public new static readonly IValueConverter ToString = new DateTimeToStringConvertor(DateTimeCovertor.To.Local);
        public static readonly IValueConverter ToString_Smart = new DateTimeToString_SmartConvertor(DateTimeCovertor.From.UTC, DateTimeCovertor.To.Local);
		public static readonly IValueConverter ToDateTime = new DateTimeToLocalTimeConverter();
    }
    public static class LocalTime
    {
        public new static readonly IValueConverter ToString = UTC.ToString;
        public static readonly IValueConverter ToString_Smart = new DateTimeToString_SmartConvertor(DateTimeCovertor.From.Local, DateTimeCovertor.To.Local);
    }

    public static class ToUTC
    {
        public new static readonly IValueConverter ToString = new DateTimeToStringConvertor(DateTimeCovertor.To.UTC);
        public static readonly IValueConverter ToString_Smart = new DateTimeToString_SmartConvertor(DateTimeCovertor.From.Local, DateTimeCovertor.To.UTC);
    }

}
