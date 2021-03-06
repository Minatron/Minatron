﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace CameraController
{
    public class WebResult
    {
        private readonly JObject _obj;

        public WebResult(JObject obj)
        {
            _obj = obj;
        }

        protected JObject Obj
        {
            get { return _obj; }
        }
    }

    public class LoginResult:WebResult
    {
        public LoginResult(JObject obj) : base(obj)
        {
            
        }

        public string SessionId
        {
            get { return (string) Obj["sid"]; }
        }
    }

    public class Channel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Address{ get; set; }
    }

    public class ChannelsResult:WebResult
    {
        public ChannelsResult(JObject obj) : base(obj)
        {
            
        }
        public IEnumerable<Channel> Channels
        {
            get
            {
               var array = ((JArray) Obj["result"]);
               return  array.Select((t, i) => new Channel()
                                                     {
                                                         
                                                         Address = (string)t[2],
                                                         Name = (string)t[0],
                                                         Id = (string)t[1]
                                                     }).ToList();
            }
        }

        public Channel GetChannelByName(string name)
        {
            return Channels.ToList().Find(each => each.Name == name);
        }

        public Channel GetChannelByID(string s)
        {
            return Channels.ToList().Find(each => each.Id == s);
        }
    }



    public class ArchiveNabvigationResult: WebResult
    {
        public ArchiveNabvigationResult(JObject obj) : base(obj)
        {
        }
        public int Result
        {
            get { return Int32.Parse(Obj["result"].ToString()); }
        }
    }

    public class AtrchiveEnterResult : ArchiveNabvigationResult
    {
        public AtrchiveEnterResult(JObject obj) : base(obj)
        {
        }
        
        public string VideoAddress
        {
            get { return (string) Obj["va"]; }
        }
    }

    public class StatusResult : WebResult
    {
        public StatusResult(JObject obj) : base(obj)
        {

        }

        public string GetStatus(Channel cnannel)
        {
            return (string) Obj[cnannel.Id];
        }
    }
}
