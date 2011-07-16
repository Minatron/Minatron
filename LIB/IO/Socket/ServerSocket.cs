using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace Lacross.IO.Sockets
{
    public class ServerSocket : IDisposable
    {
        #region IDisposable
        public void Close()
        {
            Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~ServerSocket()
        {
            Dispose(false);
        }
        private bool m_disposed = false;
        private void Dispose(bool disposing)
        {
            if (!m_disposed)
            {                
                Destructor_managed_resources();
                if (disposing)
                {
                    Destructor_unmanaged_resources();
                }
                m_disposed = true;
            }
        }
        #endregion

        TcpListener m_tcpserver;
        public bool IsStarted
        {
            get { return m_Started; }
        }
        bool m_Started = false;

        public ServerSocket(int port)
        {
            m_tcpserver = new TcpListener(port);
        }
        private void Destructor_unmanaged_resources()
        {
            Stop();
        }
        private void Destructor_managed_resources()
        {
            m_tcpserver = null;
        }


        public bool Start()
        {
            if (m_Started) return false;

            try
            {              
                m_tcpserver.Start(10);
                m_Started = true;
            }
            catch (Exception)
            {               
                m_Started = false;
            }
            return m_Started;
        }        

        public void Stop()
        {
            if (m_tcpserver != null) m_tcpserver.Stop();
            m_Started = false;
        }

        public ClientSocket MustGetConnection()
        {
            if (!m_Started) return null;
            TcpClient con=null;
            try
            {
                con = m_tcpserver.AcceptTcpClient();
            }
            catch { }
            if (con == null) return null;
            return new ClientSocket(con);
        }

        public ClientSocket GetConnection()
        {
            if (!m_Started) return null;
            if (!m_tcpserver.Pending()) return null;
            return MustGetConnection();
        }

        public bool Pending
        {
            get
            {
                if (!m_Started) return false;
                if (m_tcpserver == null) return false;
                return m_tcpserver.Pending();
            }
        }
    }
}
