using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace CameraController
{

  
    public class Camera
    {
        public delegate void JPEGReady(byte[] jpeg);
        public delegate void StatusReady(string status);
        private readonly WebCommander _commander;
        private readonly LoginResult _session;
        private readonly Channel _data;
       


        public event JPEGReady OnJPEGReady;
        public event StatusReady OnStatusReady;

        public void InvokeOnStatusReady(string status)
        {
            StatusReady handler = OnStatusReady;
            if (handler != null) handler(status);
           
        }

        public bool InArchiveMode { get; private set; }
        public bool IsPlaying { get; private set; }

        public void SetStatus(StatusResult sr)
        {
            if(sr!=null) InvokeOnStatusReady(sr.GetStatus(_data));
        }

        public Channel Data
        {
            get { return _data; }
        }

        public byte[] CurrentJpeg { get; private set; }

        public void InvokeOnJPEGReady(byte[] jpeg)
        {
            if (IsFreeze) return;
            
            CurrentJpeg = jpeg;
            var handler = OnJPEGReady;
            if (handler != null) handler(jpeg);
            Trace.WriteLine(Data.Name);
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
        public bool ToArchiveUnFreeze(DateTime time)
        {
            if (_currentFrameTime == time) return false;
            UnFreeze();
            try
            {
                StopPlay();
                if (!InArchiveMode)
                {
                    var enterResult = _commander.ArchiveEnter(_session, Data, true);

                    if (enterResult.Result <= 0) throw new Exception();
                }
                _currentFrameTime = time;
                var seekResult = _commander.ArchiveSeek(_session, Data, time);
                if (seekResult.Result <= 0) throw new Exception();
                ThreadPool.QueueUserWorkItem(ShowFrame);
                InArchiveMode = true;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool ToArchive(DateTime time)
        {
          
            if (IsFreeze) return false;
            try
            {
                StopPlay();
                _currentFrameTime = time;
                var seekResult = _commander.ArchiveSeek(_session, Data, time);
                if (seekResult.Result <= 0) throw new Exception();
                ThreadPool.QueueUserWorkItem(ShowFrame);
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

        public void ArchiveNextFrame()
        {
            if (IsFreeze) return;
            if (!InArchiveMode) return;
            StopPlay();
            ThreadPool.QueueUserWorkItem(ShowFrame);
        }

        public void ArchivePrevFrame()
        {
            if (IsFreeze) return;
            if (!InArchiveMode) return;
            StopPlay();
            ThreadPool.QueueUserWorkItem(ShowFrame);           
        }

        private bool _stop = false;
        private object _lockObject = new object();
        private DateTime _currentFrameTime = DateTime.Now;

       
        private void GetFrameProc(object state)
        {
           // lock (_lockObject)
         //   {
                _stop = false;
                IsPlaying = true;
                while (!_stop)
                {
                    ShowFrame(null);
                }
                IsPlaying = false;
          //  }
            
        }


        private void StartPlay()
        {

            ThreadPool.QueueUserWorkItem(GetFrameProc);
            
        }

        public void StopPlay()
        {
            _stop = true;
            //lock (_lockObject)
            //{
                
            //}
           
        }

        public bool IsFreeze { get; private set; }

        public void Freeze()
        {
            if( !InArchiveMode) return;
            StopPlay();
            //StopPlayArchive();
            IsFreeze = true;
        }
        public void UnFreeze()
        {
            IsFreeze = false;
        }
    }
}
