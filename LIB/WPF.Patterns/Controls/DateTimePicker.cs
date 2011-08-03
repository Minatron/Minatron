using System;
using System.Windows;
using System.Windows.Controls;
using Band.WPF.Extensions;

namespace Band.WPF.Controls
{
    public class DateTimePicker : DatePicker
    {
        DateTime? _time;

        public static DependencyProperty SelectedDateTimeProperty;

        static DateTimePicker()
        {
            SelectedDateTimeProperty = DependencyProperty.Register("SelectedDateTime", typeof(DateTime?), typeof(DateTimePicker),
                new FrameworkPropertyMetadata(OnSelectedDateTimeChanged));
        }

        public DateTime? SelectedDateTime
        {
            get
            {
                return (DateTime?)GetValue(SelectedDateTimeProperty);
            }
            set
            {
                SetValue(SelectedDateTimeProperty, value);
            }
        }

        static void OnSelectedDateTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DateTimePicker picker = (DateTimePicker)sender;
            picker._time = (DateTime?)e.NewValue;

            var selectedDate = picker.SelectedDate;
            if (!selectedDate.IsSameDate(picker._time))
            {
                picker.SelectedDate = picker._time;
            }
        }

        protected override void OnSelectedDateChanged(SelectionChangedEventArgs e)
        {
            var date = SelectedDate;
            if (date.HasValue && _time.HasValue)
            {
                SelectedDateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, _time.Value.Hour, _time.Value.Minute, _time.Value.Second, _time.Value.Millisecond);
            }
            else
            {
                SelectedDateTime = date;
            }
            base.OnSelectedDateChanged(e);
        }
    }
}
