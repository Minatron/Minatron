using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using Band.CameraNavigator.Module.View;
using Band.Client.Infrastructure.Events;
using Band.Storage.Minatron;
using Band.WPF.Commands;
using CameraController;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;

namespace Band.CameraNavigator.Module.Presenter
{

    public class DoubleCamera
    {
        public CameraPresenter Camera1 { get; set; }
        public CameraPresenter Camera2 { get; set; }

    }
    public class ControllerPresenter : INotifyPropertyChanged 
    {
        private readonly Controller _controller;
        private readonly IEventAggregator _eventAgrigator;
        public ControllerPresenter(Controller controller, IEventAggregator eventAgrigator)
        {
            _controller = controller;
            _controller.OnException += new Controller.ControllerException(_controller_OnException);
            _eventAgrigator = eventAgrigator;
            _eventAgrigator.GetEvent<ShowMovieForWeightDataEvent>().Subscribe(ProcessWeightData,ThreadOption.UIThread,true);
            _eventAgrigator.GetEvent<FreezeEvent>().Subscribe(e => UpdateReport());
            Play = new DelegateCommand(PlayInvoke,()=>controller.IsLogin);
            ToStartTime = new DelegateCommand<object>(ToStartInvoke, e => controller.IsLogin);
            Stop = new DelegateCommand(StopInvoke, () => controller.IsLogin);
            FrameNext = new DelegateCommand(FrameNextInvoke, ()=> controller.IsLogin);
            FramePrev = new DelegateCommand(FramePrevInvoke, () => controller.IsLogin);
            //_count = ControllerCameras.Count;
            InvokePropertyChanged("ControllerCameras");
        }

        void _controller_OnException(ControllerExceptions ex)
        {
            ControllerException = ex;
            InvokePropertyChanged("ControllerException");
        }

        private void ToStartInvoke(object obj)
        {
            _controller.SeekToTime(WData.WeighTime);
            Refresh();
        }

        public ControllerExceptions ControllerException { get; private set; }
        private void UpdateReport()
        {
            var view = new CamerasReportView(this);
            GreateXDocument((FlowDocument) view.Content);
        }
        private Uri _packageUri = null;
        public void GreateXDocument(FlowDocument data)
        {
            if (_packageUri != null) PackageStore.RemovePackage(_packageUri);

            var xpsStream = new MemoryStream();
            var package = Package.Open(xpsStream, FileMode.Create, FileAccess.ReadWrite);                
            const string memorystreamDataXps = "memorystream://data.xps";
            _packageUri = new Uri(memorystreamDataXps);
            PackageStore.AddPackage(_packageUri, package);
            var xpsDocument = new XpsDocument(package, CompressionOption.Maximum, memorystreamDataXps);
            var writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            var printTicket = new PrintTicket {PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4)};
            printTicket.PageResolution = new PageResolution(150, 150);
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Loaded,
                                                new DispatcherOperationCallback(arg => null), null);
            writer.Write(((IDocumentPaginatorSource) data).DocumentPaginator, printTicket);
            Document = xpsDocument.GetFixedDocumentSequence();
            //xpsDocument.Close();
            InvokePropertyChanged("Document"); 
        }

        public FixedDocumentSequence Document { get; private set; }

        public List<CameraPresenter> FreezedImages
        {
            get
            {
                return _allCamerasPresenter == null
                           ? null
                           : _allCamerasPresenter.FindAll(
                               each =>
                               _controller.CurrentDirectionCameras.Exists(res => each.Camera == res && each.IsFreeze));
            }
        }

        private void ProcessWeightData(WeighData data)
        {
            WData = data; 
            _controller.SetArchiveToTimeAndDirection((int) data.Course,data.WeighTime);
            InvokePropertyChanged("ControllerCameras");
            InvokePropertyChanged("FreezedImages");
            Refresh();
            UpdateReport();

        }

        public WeighData WData { get; private set; }

        private void PlayInvoke()
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

        private void StopInvoke()
        {
            _controller.StopPlayArchive();
            Refresh();
        }

        private void FrameNextInvoke()
        {
            _controller.NextFrameArchive();
            Refresh();
        }

        private void FramePrevInvoke()
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
        private int _count;


        

        public List<DoubleCamera> ControllerCameras
        {
            get
            {
                if (_allCamerasPresenter == null && _controller.Cameras.Count>0)
                    _allCamerasPresenter = _controller.Cameras.ConvertAll(each => new CameraPresenter(each,_eventAgrigator));
                var c = _controller.CurrentDirectionCameras.Select(camera => _allCamerasPresenter.Find(each => each.Camera == camera)).Where(f => f != null).ToList();


                var ret = new List<DoubleCamera>();
                  var i = 0;
                    while (i < c.Count)
                    {
                        var dc = new DoubleCamera();
                        dc.Camera1 = c[i];
                        i++;
                        if (i < c.Count) { dc.Camera2 = c[i];
                            i++;
                            
                        }
                        ret.Add(dc);
                    }
                
                return ret;
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
