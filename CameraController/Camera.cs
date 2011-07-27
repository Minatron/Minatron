using System;
using System.Threading;

namespace CameraController
{

    public delegate void JPEGReady(byte[] jpeg);
    public class Camera
    {
        private readonly WebCommander _commander;
        private readonly LoginResult _session;
        private readonly Channel _data;
       


        public event JPEGReady OnJPEGReady;

        public bool InArchiveMode { get; private set; }
        public bool IsPlaying { get; private set; }

        public Channel Data
        {
            get { return _data; }
        }

        public byte[] CurrentJpeg { get; private set; }

        public void InvokeOnJPEGReady(byte[] jpeg)
        {
            CurrentJpeg = jpeg;
            var handler = OnJPEGReady;
            if (handler != null) handler(jpeg);
        }

        public Camera(WebCommander commander, LoginResult session, Channel data)
        {
            InArchiveMode = false;
            _commander = commander;
            _session = session;
            _data = data;

        }

     


        private void ShowFrame(object state)
        {
            try
            {
                
                InvokeOnJPEGReady(_commander.GetJPEG(_session, Data));
            }
            catch (Exception ex)
            {
                               
            }
        }

        public bool ToArchive(DateTime time)
        {
            if (IsFreeze) return false;
            try
            {
                StopPlay();
                if (!InArchiveMode)
                {
                    var enterResult = _commander.ArchiveEnter(_session, Data, true);
                    if (enterResult.Result <= 0) throw new Exception();
                }
                var seekResult = _commander.ArchiveSeek(_session, Data, time);
                if (seekResult.Result <= 0) throw new Exception();
                ShowFrame(null);
                InArchiveMode = true;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool ToRealTime()
        {
            if (IsFreeze) return false;
            try
            {
                if (InArchiveMode)
                {
                    var enterResult = _commander.ArchiveEnter(_session, Data, false);
                    if (enterResult.Result <= 0) throw new Exception();
                }
                StartPlay();
                InArchiveMode = false;
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public bool PlayArchive()
        {
            if (IsFreeze) return false;
            if (!InArchiveMode) return false;
            try
            {
                var res  = _commander.ArchivePlay(_session, Data);
                if (res.Result<=0) throw new Exception();
                StartPlay();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool StopPlayArchive()
        {
            if (IsFreeze) return false;
            if (!InArchiveMode) return false;
            try
            {
                var res = _commander.ArchiveStop(_session, Data);
                if (res.Result <= 0) throw new Exception();
                StopPlay();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool ArchiveNextFrame()
        {
            if (IsFreeze) return false;
            if (!InArchiveMode) return false;
            StopPlay();
            try
            {
                var res = _commander.ArchiveFrameNext(_session, Data);
                if (res.Result <= 0) throw new Exception();
                ShowFrame(null);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool ArchivePrevFrame()
        {
            if (IsFreeze) return false;
            if (!InArchiveMode) return false;
            StopPlay();
            try
            {
                var res = _commander.ArchiveFramePrev(_session, Data);
                if (res.Result <= 0) throw new Exception();
                ShowFrame(null);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool _stop = false;
        private void GetFrameProc(object state)
        {
            _stop = false;
            IsPlaying = true;
            while (!_stop)
            {
                ShowFrame(null);
            }
            IsPlaying = false;
        }


        private void StartPlay()
        {

            ThreadPool.QueueUserWorkItem(GetFrameProc);
            
        }

        public void StopPlay()
        {
            _stop = true;
           
        }

        protected bool IsFreeze { get; private set; }
    }
}
