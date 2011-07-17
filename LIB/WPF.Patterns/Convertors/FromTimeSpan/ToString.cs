using System;
using System.Text;
using System.Windows.Data;
using Band.WPF.Localization;

namespace WPF.Patterns.Convertors
{
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public class TimeSpanToStringConvertor : IValueConverter
    {
        readonly int MaxCount;
        readonly bool WithMiliseconds;
        public TimeSpanToStringConvertor()
        {
            MaxCount = 100;
            WithMiliseconds = true;
        }

        public TimeSpanToStringConvertor(int maxCount)
        {
            MaxCount = maxCount;
            WithMiliseconds = false;
        }

        static string[] DefaultLabels = new string[]{"d ","h ","m ","s"};
            
        static int Append(StringBuilder res,int v, string m)
        {
            if (v >0) 
            {
                res.Append(v);
                res.Append(m);
                return 1;
            }
            return 0;
        }
        static string[] ConvertLabels(string sp)
        {
            if (string.IsNullOrEmpty(sp)) sp = Lang.GetTitle(@"TimeSpan/Template");
            if ( string.IsNullOrEmpty(sp)) return DefaultLabels;            
            string[] res = sp.Split('-');
            if (res.Length < 4) return DefaultLabels;
            return res;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			if (value == null) return null;
            string[] labels= ConvertLabels((string)parameter);            
            StringBuilder result = new StringBuilder();
            TimeSpan span = (TimeSpan)value;
            int count = 0;

            count += Append(result, span.Days, labels[0]);
            if (count < MaxCount) count += Append(result, span.Hours, labels[1]); else return result.ToString();
            if (count < MaxCount) count += Append(result, span.Minutes, labels[2]); else return result.ToString();
            
            if (count < MaxCount)
            {
                if (!WithMiliseconds || (span.Milliseconds == 0)) count += Append(result, span.Seconds, labels[3]);
                else
                {
                    double sec = span.Seconds + ((double)span.Milliseconds / 1000.0);        
                    result.Append(sec.ToString("0.###"));                    
                    result.Append(labels[3]);
                    count ++;
                }
            }
            
            

            if (count == 0) 
            {
                result.Append((int)0);
                result.Append(labels[3]);
            }     
            return result.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
