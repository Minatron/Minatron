using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace CameraController
{
    public class WebCommander
    {
        private readonly string _server;
        private readonly string _videoServer;
        
        public WebCommander(string server, string videoServer)
        {
            _server = server;
            _videoServer = videoServer;
        }
        
        private byte[] SendCommand(string req)
        {
            var request = (HttpWebRequest) WebRequest.Create(req);

            var responseTask = Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);

            if (((HttpWebResponse)responseTask.Result).StatusCode == HttpStatusCode.OK)
            {

                var res = responseTask.Result.ContentLength > 0 ? new byte[responseTask.Result.ContentLength] : new byte[1024];
                using (var stream = responseTask.Result.GetResponseStream())
                {
                    var countRead = 0;
                    var offset = 0;
                    var len = (responseTask.Result.ContentLength > 0 ? responseTask.Result.ContentLength : 1024);
                    while ((countRead = stream.Read(res, offset, (int)(len - offset))) > 0)
                    {
                        offset += countRead;
                    }
                    return res;
                }

            }
            return null;
            //var response = (HttpWebResponse)request.GetResponse();

            //var offset = 0;

            //if (response.StatusCode == HttpStatusCode.OK)
            //{

            //    var res = response.ContentLength>0?new byte[response.ContentLength]:new byte[1024];
            //    var stream = response.GetResponseStream();
            //    var countRead = 0;
            //    var len = (response.ContentLength > 0 ? response.ContentLength : 1024);
            //    while ((countRead = stream.Read(res, offset, (int) (len - offset))) > 0)
            //    {
            //        offset += countRead;
            //    }
            //    stream.Dispose();
            //    response.Close();

            //    return res;


            //}
            //return null;
        }

        public void Abort()
        {
           
        }

        private JObject SendCommandReturnJson(string req)
        {
          var result = JObject.Parse(Encoding.UTF8.GetString(SendCommand(req)));
          return result;
        }

        public LoginResult Login( string login, string pass)
        {
            var cr = SendCommandReturnJson(_server + "/auth?login=" + login + "&password=" + pass+"&type=jpeg");
            return cr!=null ? new LoginResult(cr) : null;
        }

        public ChannelsResult GetChannels( LoginResult sid)
        {
            var cr = SendCommandReturnJson(_server + "/get_channels?sid="+sid.SessionId);
            return cr != null ? new ChannelsResult(cr) : null;
        }

        public AtrchiveEnterResult ArchiveEnter( LoginResult lr, Channel channel, bool enterflag, string sip = "127.0.0.1")
        {
            var cr = SendCommandReturnJson(_server + "/archive_enter?sid=" + lr.SessionId + "&cid=" + channel.Id + "&enter=" + enterflag.ToString().ToLower()+"&sip="+sip);
            return cr != null ? new AtrchiveEnterResult(cr) : null;
        }

        public ArchiveNabvigationResult ArchivePlay( LoginResult lr, Channel channel)
        {
            var cr = SendCommandReturnJson(_server + "/play?sid=" + lr.SessionId + "&cid=" + channel.Id);
            return cr != null ? new ArchiveNabvigationResult(cr) : null;
        }
        public ArchiveNabvigationResult ArchiveStop( LoginResult lr, Channel channel)
        {
            var cr = SendCommandReturnJson(_server + "/stop?sid=" + lr.SessionId + "&cid=" + channel.Id);
            return cr != null ? new ArchiveNabvigationResult(cr) : null;
        }
        public ArchiveNabvigationResult ArchiveFrameNext( LoginResult lr, Channel channel)
        {
            var cr = SendCommandReturnJson(_server + "/frame_next?sid=" + lr.SessionId + "&cid=" + channel.Id + "&tid=" + channel.Id.Replace("-", ""));
            return cr != null ? new ArchiveNabvigationResult(cr) : null;
        }
        public ArchiveNabvigationResult ArchiveFramePrev( LoginResult lr, Channel channel)
        {
            var cr = SendCommandReturnJson(_server + "/frame_prev?sid=" + lr.SessionId + "&cid=" + channel.Id + "&tid=" + channel.Id.Replace("-", ""));
            return cr != null ? new ArchiveNabvigationResult(cr) : null;
        }
        public ArchiveNabvigationResult ArchiveSeek( LoginResult lr, Channel channel, DateTime seek)
        {
            var cr = SendCommandReturnJson(_server + "/seek?sid=" + lr.SessionId + "&cid=" + channel.Id + "&tid=" + channel.Id.Replace("-", "") + "&t=" + seek.ToString("yyyy-MM-dd" + "'%'20" + "HH:mm:ss.ffffff") + "&not_found_dir=1" + "&dojo.preventCache=" + DateTime.Now.Ticks / 10000);
            return cr != null ? new ArchiveNabvigationResult(cr) : null;
        }

        public  StatusResult GetStatus(LoginResult lr, Channel channel)
        {
            JObject cr = null;
            try
            {
                    cr = SendCommandReturnJson(_server + "/get_status?sid=" + lr.SessionId + "&tid=" + channel.Id.Replace("-", "") + "&dojo.preventCache=" + DateTime.Now.Ticks / 10000);
            }
            catch (Exception)
            {
                    
                    
            }
            return cr != null ? new StatusResult(cr) : null;
        }

        private object lockobject = new object();
        public byte[] GetJPEG( LoginResult lr, Channel channel)
        {
          //  lock (lockobject)
           // {
                byte[] res = null;
                try
                {
                    res =
                        SendCommand(_videoServer + "/jpeg/" + channel.Address + "/u?id=" + lr.SessionId + "/" +
                                    channel.Id + "&t=" + DateTime.Now.Ticks/10000);
                }
                catch (Exception)
                {

                }
                return res;
          // }
        }

    }
}
