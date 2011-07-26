using System.Collections.Generic;
using System.ComponentModel;
using Band.Client.Infrastructure.Events;
using Band.Storage.Minatron;
using CameraController;
using Microsoft.Practices.Composite.Events;

namespace Band.CameraNavigator.Module.Presenter
{
    public class ControllerPresenter : INotifyPropertyChanged 
    {
        private readonly Controller _controller;
        private readonly IEventAggregator _eventAgrigator;
        public ControllerPresenter(Controller controller, IEventAggregator eventAgrigator)
        {
            _controller = controller;
            _eventAgrigator = eventAgrigator;
            _eventAgrigator.GetEvent<ShowMovieForWeightDataEvent>().Subscribe(ProcessWeightData);
            _controller.SetToRealTime();
            InvokePropertyChanged("ControllerCameras");
        }

        private void ProcessWeightData(WeighData data)
        {
            _controller.SetArchiveToTimeAndDirection((int) data.Course,data.WeighTime);
            InvokePropertyChanged("ControllerCameras");

        }

        public List<CameraPresenter> ControllerCameras
        {
            get
            {
                return _controller.CurrentDirectionCameras.ConvertAll(each=>new CameraPresenter(each));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
