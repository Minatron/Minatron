using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Band.Client.Infrastructure.Events;
using Band.Storage.Minatron;
using CameraController;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Presentation.Events;

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
            _eventAgrigator.GetEvent<ShowMovieForWeightDataEvent>().Subscribe(ProcessWeightData,ThreadOption.UIThread,true);
            _eventAgrigator.GetEvent<FreezeEvent>().Subscribe(e=>InvokePropertyChanged("FreezedImages"));
            _controller.SetToRealTime();
            Play = new DelegateCommand<object>(PlayInvoke);
            ToStartTime = new DelegateCommand<object>(ToStartInvoke);
            Stop = new DelegateCommand<object>(StopInvoke);
            FrameNext = new DelegateCommand<object>(FrameNextInvoke);
            FramePrev = new DelegateCommand<object>(FramePrevInvoke);
            InvokePropertyChanged("ControllerCameras");
        }

        private void ToStartInvoke(object obj)
        {
            _controller.SeekToTime(WData.WeighTime);
            Refresh();
        }


        public List<CameraPresenter> FreezedImages
        {
            get { return ControllerCameras.FindAll(each => each.Camera.IsFreeze); }
        }

        private void ProcessWeightData(WeighData data)
        {
            WData = data; 
            _controller.SetArchiveToTimeAndDirection((int) data.Course,data.WeighTime);
            InvokePropertyChanged("ControllerCameras");
            InvokePropertyChanged("FreezedImages");
            Refresh();

        }

        public WeighData WData { get; private set; }

        private void PlayInvoke(object e)
        {
            _controller.PlayArchive();
            Refresh();
        }

        private void Refresh()
        {
            InvokePropertyChanged("IsPlayingArchive");
            InvokePropertyChanged("IsStopPlayingArchive");
            InvokePropertyChanged("IsRealMode");
            InvokePropertyChanged("WData");
        }

        private void StopInvoke(object e)
        {
            _controller.StopPlayArchive();
            Refresh();
        }

        private void FrameNextInvoke(object e)
        {
            _controller.NextFrameArchive();
            Refresh();
        }

        private void FramePrevInvoke(object e)
        {
            _controller.PrevFrameArchive();
            Refresh();
        }
        public ICommand Play { get; private set; }
        public ICommand ToStartTime { get; private set; }
        public ICommand Stop { get; private set; }
        public ICommand FrameNext { get; private set; }
        public ICommand FramePrev { get; private set; }
        public bool IsPlayingArchive
        {
            get { return !_controller.IsPlayingArchive; }
        }

        public bool IsStopPlayingArchive
        {
            get { return _controller.IsPlayingArchive; }
        }

        public bool IsRealMode
        {
            get { return _controller.IsRealMode; }
        }


        private List<CameraPresenter> _allCamerasPresenter;

        public List<CameraPresenter> ControllerCameras
        {
            get
            {
                if (_allCamerasPresenter == null)
                    _allCamerasPresenter = _controller.Cameras.ConvertAll(each => new CameraPresenter(each,_eventAgrigator));

                return _allCamerasPresenter.FindAll(each => _controller.CurrentDirectionCameras.Exists(res=>each.Camera==res));

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
