using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CameraController;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCameraNavigator
{
    [TestClass]
    public class UnitTest1
    {
        private const string _server = "http://ns.e105.ru:3082";
        private const string _video = "http://ns.e105.ru:3084";
       
        [TestMethod]
        public void Test()
        {
            var commnader = new WebCommander(_server,_video);
            var res = commnader.Login( "admin", "");
            Assert.AreNotEqual(null, res);
            var channels = commnader.GetChannels( res);
            Assert.AreNotEqual(null, channels.Channels);
            var channel = 
            channels.Channels.ToArray()[0];
            var archiveEnter = commnader.ArchiveEnter( res, channel, true);
            Assert.IsTrue(archiveEnter.Result > 0);
            var archiveSeek = commnader.ArchiveSeek( res, channel, new DateTime(2011, 07, 23, 1, 0, 0));
            Assert.IsTrue(archiveSeek.Result > 0);
            var jpeg = commnader.GetJPEG( res, channel);
        }
    }
}
