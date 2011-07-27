using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CameraController
{
    public class CamerasConfigurator
    {
        private SerializableDictionary<int, List<string>> _camerasToDirection;
        public CamerasConfigurator(string xml)
        {
            if(!CreateFromString(xml)) _camerasToDirection = new SerializableDictionary<int, List<string>>();
        }

        public CamerasConfigurator()
        {
           _camerasToDirection = new SerializableDictionary<int, List<string>>();
        }

        private bool CreateFromString(string xml)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(SerializableDictionary<int, List<string>>));
                _camerasToDirection = (SerializableDictionary<int, List<string>>)serializer.Deserialize(new StringReader(xml));
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public string GetSaveString()
        {
            var serializer = new XmlSerializer(typeof(SerializableDictionary<int, List<string>>));
            var sw = new StringWriter();
            serializer.Serialize(sw, _camerasToDirection);
            return sw.ToString();
        }

        public bool AddCameraToDirection(Channel channel, int dir)
        {
            if(_camerasToDirection.ContainsKey(dir))
            {
               if( _camerasToDirection[dir].Exists(each=>channel.Id==each))
               {
                   return false;
               }
                _camerasToDirection[dir].Add(channel.Id);
                return true;
            }
            _camerasToDirection.Add(dir,new List<string> {channel.Id});
            return true;
        }

        public List<string> GetCamerasForDirection(int dir)
        {
            return _camerasToDirection.ContainsKey(dir) ? _camerasToDirection[dir] : null;
        }
    }
}
