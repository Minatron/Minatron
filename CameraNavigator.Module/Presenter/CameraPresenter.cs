using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Band.Client.Infrastructure.Events;
using CameraController;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace Band.CameraNavigator.Module.Presenter
{
    public class CameraPresenter : INotifyPropertyChanged 
    {
        private readonly Camera _camera;
        private readonly IEventAggregator _eventAgrigator;


        public CameraPresenter(Camera camera, IEventAggregator eventAgrigator)
        {
            _camera = camera;
            _eventAgrigator = eventAgrigator;
            Camera.OnJPEGReady += _camera_OnJPEGReady;
            Camera.OnStatusReady += _camera_OnStatusReady;
            FreezeCamera = new DelegateCommand<object>(InvokeFreezCamera);
        }

        public bool IsFreeze { get { return Camera.IsFreeze; }}


        public void InvokeFreezCamera(object obj)
        {
            if (Camera.IsFreeze) Camera.UnFreeze();
            else Camera.Freeze();
            _eventAgrigator.GetEvent<FreezeEvent>().Publish(null);
            InvokePropertyChanged("IsFreeze");
        }

        void _camera_OnStatusReady(string status)
        {
            Status = status;
            InvokePropertyChanged("Status");
        }

        
        public string Status { get; private set; }
        public string CameraName{ get { return Camera.Data.Name; }}

        void _camera_OnJPEGReady(byte[] jpeg)
        {
            var memoryStream = new MemoryStream(jpeg);
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            bitmap.Freeze();
            CameraImage  = bitmap;
            InvokePropertyChanged("CameraImage");

            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Render, new DispatcherOperationCallback(arg => null), null);
        }

        public ICommand FreezeCamera { get; private set; }
        
        public ImageSource CameraImage
        {
            get; private set;
        }

        public Camera Camera
        {
            get { return _camera; }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void InvokePropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
