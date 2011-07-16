using System;
using System.Windows.Threading;

namespace WPF.Patterns.ViewModel
{
    public static class AutoRefresher
    {
        public delegate void AutoRefreshDelegate();

        static DispatcherTimer timer;

        static AutoRefresher()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += delegate
                {
                    evAutoRefresh();
                };
                
            timer.Start();
        }

        public static event AutoRefreshDelegate evAutoRefresh = delegate { };

        public static TimeSpan Interval
        {
            get
            {
                return timer.Interval;
            }
            set
            {
                timer.Interval = value;
            }
        }
    }
}
