using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CameraController
{
    public class Controller
    {
        private  CamerasConfigurator _configurator;
        private readonly string _login;
        private readonly string _pass;
        private WebCommander _commander;
        private List<Camera> _cameras = new List<Camera>();
        private LoginResult _session;
        private int _dir;

        public bool IsLogin { 
            get { return _session != null&&_session.SessionId!=""; }
        }
        public Controller(CamerasConfigurator configurator, string server, string video, string login, string pass)
        {
            IsRealMode = false;
            _configurator = configurator;
            _login = login;
            _pass = pass;
            _commander = new WebCommander(server, video);
            Init();
        }


        private void Init()
        {
            try
            {
                _session = _commander.Login(_login, _pass);
            }
            catch (Exception)
            {

            }
            if (IsLogin)
            {
                try
                {
                    var channels = _commander.GetChannels(_session);
                    if (_configurator == null)
                    {
                     
                        _configurator = new CamerasConfigurator();
                           channels.Channels.ToList().ForEach(each=>_configurator.AddCameraToDirection(each,1));
                    //    _configurator.AddCameraToDirection(channels.GetChannelByID("5e73972f-3b78-4c96-b1fb-8e97632bea2e"), 0);
                    //    _configurator.AddCameraToDirection(channels.GetChannelByID("ac998b73-38d5-4cdd-bf4a-1d6ca9dce859"), 0);
                    //    _configurator.AddCameraToDirection(channels.GetChannelByID("5cdec79f-6552-4e11-995d-1c0736513ecf"), 0);
                    //    _configurator.AddCameraToDirection(channels.GetChannelByID("ef4fc01e-a8f3-474b-b9fb-7ebc81c5364d"), 1);
                    //    _configurator.AddCameraToDirection(channels.GetChannelByID("e5b86eb0-07a8-4d57-b2a2-1321ab981955"), 1);
                    //    _configurator.AddCameraToDirection(channels.GetChannelByID("b3d506ab-4a7e-4a9b-a47d-0eb87b6cbf98"), 1);
                    }
                    channels.Channels.ToList().ForEach(each=> Cameras.Add(new Camera(_commander,_session,each)));
                }
                catch (Exception)
                {

                }

            }
        }
        public void SetArchiveToTimeAndDirection(int dir, DateTime time)
        {
           
            if (IsRealMode)
            {
                CurrentDirectionCameras.ForEach(camera => camera.StopPlay());
            }
            else
            {
                StopPlayArchive();
            }
            IsRealMode = false;
            _dir = dir;            
            CurrentDirectionCameras.ForEach(camera=> camera.ToArchiveUnFreeze(time));
            StartGetStatus();
        }

        public void SeekToTime(DateTime time)
        {
            if(!IsArchiveMode) return;
            StopPlayArchive();
            CurrentDirectionCameras.ForEach(camera => camera.ToArchive(time));
        }

        private bool _stopGetStatus = true;
        private void GetStatus(object state)
        {
            _stopGetStatus = false;
            while (!_stopGetStatus)
            {
                Thread.Sleep(500);
                try
                {
                    var sr = _commander.GetStatus(_session, CurrentDirectionCameras[0].Data);
                   CurrentDirectionCameras.ForEach(each=>each.SetStatus(sr));
                }
                catch (Exception)
                {

                }
            }
        }

        private void StartGetStatus()
        {
            if (!_stopGetStatus) return;
            ThreadPool.QueueUserWorkItem(GetStatus);
        }
        private void StopGetStatus()
        {
            _stopGetStatus = true;
        }



        public void PlayArchive()
        {
            if (IsRealMode) return;
            if (IsPlayingArchive) return;
            ThreadPool.QueueUserWorkItem(StartPlayArchive);
            IsPlayingArchive = true;
        }

        private void StartPlayArchive(object state)
        {
          
            CurrentDirectionCameras.ForEach(camera => camera.PlayArchive());

        }

        public void StopPlayArchive()
        {
            if (IsRealMode) return;
            if(!IsPlayingArchive) return; 
            CurrentDirectionCameras.ForEach(camera => camera.StopPlayArchive());
            IsPlayingArchive = false;
        }

        public void NextFrameArchive()
        {
            if (IsRealMode) return;
            StopPlayArchive();
            try
            {
                var res = _commander.ArchiveFrameNext(_session, CurrentDirectionCameras[0].Data);
                if (res.Result <= 0) throw new Exception();
            }
            catch (Exception)
            {
                
                
            }
            
            CurrentDirectionCameras.ForEach(camera => camera.ArchiveNextFrame()); 
        }

        public void PrevFrameArchive()
        {
            if (IsRealMode) return;
            StopPlayArchive();
            try
            {
                var res = _commander.ArchiveFramePrev(_session, CurrentDirectionCameras[0].Data);
                  if (res.Result <= 0) throw new Exception();
            }
            catch(Exception ex)
            {

            }

            CurrentDirectionCameras.ForEach(camera => camera.ArchivePrevFrame());
        }


        public void SetToRealTime()
        {
            IsRealMode = true;
            CurrentDirectionCameras.ForEach(camera => camera.ToRealTime());
        }



        public List<Camera> CurrentDirectionCameras
        {
            get
            {
                try
                {
                    if (IsRealMode) return Cameras;
                    return
                        Cameras.FindAll(
                            each => _configurator.GetCamerasForDirection(_dir).Exists(str => str == each.Data.Id));
                }
                catch (Exception)
                {

                    return null;
                } 
               
            }
        }

        public bool IsRealMode { get; private set; }
        public bool IsArchiveMode
        {
            get { return !IsRealMode; }
        }

        public bool IsPlayingArchive { get; private set; }

        public List<Camera> Cameras
        {
            get { return _cameras; }
        }
    }
}
