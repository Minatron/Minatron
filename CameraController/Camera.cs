using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace CameraController
{
    
    public enum  CameraExceptions
    {
        None,
        EnterToArchiveError,
        SeekToTimeException,
        RealTimeEnterException,
        PlayArchiveException,
        StopArchiveException,
        LoadVideoError
    }
    public class Camera
    {
        public delegate void JPEGReady(byte[] jpeg);
        public delegate void StatusReady(string status);
        public delegate void CameraExeption(CameraExceptions exception);
        private readonly WebCommander _commander;
        private readonly LoginResult _session;
        private readonly Channel _data;
        public event JPEGReady OnJPEGReady;
        public event StatusReady OnStatusReady;
        public event CameraExeption OnException;
        public void InvokeOnException(CameraExceptions exception)
        {
            if (IsFreeze) return;
            CameraExeption handler = OnException;
            if (handler != null) handler(exception);
        }
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
            if (handler != null) handler(jpeg);;
        }
        public Camera(WebCommander commander, LoginResult session, Channel data)
        {
            InArchiveMode = false;
            _commander = commander;
            _session = session;
            _data = data;

        }
        private object lockObject = new object();
        private void ShowFrame(object state)
        {
            try
            {           
                byte[] res = null;
                res = _commander.GetJPEG(_session, Data, lockObject);
                if (res != null)
                {
                    InvokeOnException(CameraExceptions.None);
                    InvokeOnJPEGReady(res);
                }
                else
                {
                    InvokeOnException(CameraExceptions.LoadVideoError);   
                }                
            }
            catch (Exception ex)
            {
                            
            }
        }
        public bool ToArchiveUnFreeze(DateTime time)
        {
            UnFreeze();
            try
            {
                InvokeOnException(CameraExceptions.None);             
                var enterResult = _commander.ArchiveEnter(_session, Data, false);
                enterResult = _commander.ArchiveEnter(_session, Data, true);
                if (enterResult.Result <= 0 && !InArchiveMode) throw new Exception();                
                var seekResult = _commander.ArchiveSeek(_session, Data, time);
                if (seekResult.Result <= 0) throw new Exception();
                InArchiveMode = true;
                StartPlay();
            }
            catch (Exception)
            {
                InvokeOnException(CameraExceptions.EnterToArchiveError);
                return false;
            }
            return true;
        }
        public void Update()
        {
            ThreadPool.QueueUserWorkItem(ShowFrame);
        }
        public bool ToRealTime()
        {
            if (IsFreeze) return false;
            try
            {
                InvokeOnException(CameraExceptions.None);
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
                InvokeOnException(CameraExceptions.RealTimeEnterException);
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
        public bool IsFreeze { get; private set; }
        public void Freeze()
        {
            if( !InArchiveMode) return;
            IsFreeze = true;
        }
        public void UnFreeze()
        {
            IsFreeze = false;
        }
    }
}
