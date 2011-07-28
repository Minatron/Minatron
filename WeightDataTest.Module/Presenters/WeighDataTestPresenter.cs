using System;
using System.ComponentModel;
using System.Windows.Input;
using Band.Client.Infrastructure.Events;
using Band.Storage.Minatron;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace WeightDataTest.Module.Presenters
{
    public class WeighDataTestPresenter : INotifyPropertyChanged
    {
        private readonly IEventAggregator _aggregator;

        public WeighDataTestPresenter(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
            WeightTime =DateTime.Now.Subtract(new TimeSpan(4));
            InvokePropertyChanged("WeightTime");
            ShowArchive = new DelegateCommand<object>(ShowArchiveNextInvoke);

        }

        private void ShowArchiveNextInvoke(object obj)
        {
           var res = new WeighData(){AvgSpeed = (float) Speed,Course = (CourseType) Course,Weigh = (float) Weight,WeighTime = WeightTime};
           _aggregator.GetEvent<ShowMovieForWeightDataEvent>().Publish(res);
        }

        public DateTime WeightTime { get; set; }
        public double Speed{ get; set; }
        public double Weight { get; set; }
        public int Course { get; set; }

        public ICommand ShowArchive { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
