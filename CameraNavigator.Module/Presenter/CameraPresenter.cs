using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CameraController;

namespace Band.CameraNavigator.Module.Presenter
{
    public class CameraPresenter : INotifyPropertyChanged 
    {
        private readonly Camera _camera;
     

        public CameraPresenter(Camera camera)
        {
            _camera = camera;
            
            _camera.OnJPEGReady += _camera_OnJPEGReady;
           
        }

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
        }

        public ImageSource CameraImage
        {
            get; private set;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
