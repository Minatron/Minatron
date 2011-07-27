using System;
using System.Collections.Generic;
using System.Linq;

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
        private bool _realTime = false;
        public bool IsLogin { 
            get { return _session != null&&_session.SessionId!=""; }
        }
        public Controller(CamerasConfigurator configurator, string server, string video, string login, string pass)
        {
            
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
                        _configurator.AddCameraToDirection(channels.Channels.ToList()[2], 0);
                        _configurator.AddCameraToDirection(channels.Channels.ToList()[3], 0);
                        _configurator.AddCameraToDirection(channels.Channels.ToList()[1], 0);
                    }
                    channels.Channels.ToList().ForEach(each=> _cameras.Add(new Camera(_commander,_session,each)));
                }
                catch (Exception)
                {

                }

            }
        }
        public void SetArchiveToTimeAndDirection(int dir, DateTime time)
        {
            CurrentDirectionCameras.ForEach(camera => camera.StopPlay());

            _dir = dir;
            _realTime = false;
            CurrentDirectionCameras.ForEach(camera=> camera.ToArchive(time));
        }
        public void SetToRealTime()
        {
            _realTime = true;
            CurrentDirectionCameras.ForEach(camera => camera.ToRealTime());
        }
        public List<Camera> CurrentDirectionCameras
        {
            get
            {
              // if (_realTime) return _cameras;
                return
                    _cameras.FindAll(
                        each => _configurator.GetCamerasForDirection(_dir).Exists(str => str == each.Data.Id));
               
               
            }
        }
    }
}
