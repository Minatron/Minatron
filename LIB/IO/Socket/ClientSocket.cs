using System;
using System.Net.Sockets;
using System.IO;

namespace Lacross.IO.Sockets
{
    public class ClientSocket :IDisposable
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
        ~ClientSocket()
        {
            Dispose(false);
        }
        private bool m_disposed = false;
        private void Dispose(bool disposing)
        {
            if (!m_disposed)
            {                
                
                if (disposing)
                {
                    Destructor_unmanaged_resources();
                }
                Destructor_managed_resources();
                m_disposed = true;
            }
        }
        #endregion

        TcpClient m_tcpclient;
        NetworkStream m_stream = null;

		public Stream Stream
		{
			get
			{
				return m_stream;
			}
		}

        public bool IsConnected
        {
            get 
            {
                if (m_tcpclient == null) return false;
                return m_tcpclient.Connected;
            }
        }
        public ClientSocket()
        {
            m_tcpclient = new TcpClient();
        }

        public ClientSocket(TcpClient clientConnect)
        {
            m_tcpclient = clientConnect;
            if (m_tcpclient.Connected)
            {
                m_stream = m_tcpclient.GetStream();
            }
            else
            {
                CloseConnect();
            }
        }       

        private void Destructor_unmanaged_resources()
        {
            CloseConnect();
        }
        private void Destructor_managed_resources()
        {
            if (m_stream != null) m_stream.Dispose();
            m_stream = null;
            m_tcpclient = null;            
        }

        public bool Connect(string adress, int port)
        {
			if (m_tcpclient == null)
			{
				m_disposed = false;
				m_tcpclient = new TcpClient();
			}
            if (m_tcpclient.Connected) return false;

            try
            {
                m_tcpclient.Connect(adress, port);
				if (m_tcpclient.Connected)
				{
					m_stream = m_tcpclient.GetStream();
					return true;
				}
            }
            catch (SocketException)
            {
            }            
            
            return false;
        }

        public void CloseConnect()
        {
            if (m_stream != null)  m_stream.Close();
            if (m_tcpclient != null) m_tcpclient.Close();
        }
       
        #region Read
        /// <summary>
        /// Чтение из порта в масив байт
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int MustRead(byte[] buffer, int offset, int count)
        {
            int res = 0;
            if (m_tcpclient.Connected)
            {
                try
                {
                    res = m_stream.Read(buffer, offset, count);
                }
                catch
                {
                    res = -1;
                }
                if (!m_tcpclient.Connected) res = -1;
            }
            else res = -1;
            return res;

        }

        public unsafe int ReadInt16(out short value)
        {
            byte[] buffer = new byte[2];
            int res = MustRead(buffer, 0, 2);
            value = 0;
            if (res == 2)
            {
                fixed (byte* par = buffer)
                {
                    value = *((short*)par);
                }

            }

            return res;

        }

        public unsafe int ReadInt32(out int value)
        {
            byte[] buffer = new byte[4];
            int res = MustRead(buffer,0,4);
            value = 0;
            if (res == 4)
            {
               fixed (byte* par = buffer)
               {
                   value = *((int*)par);
               }
      
            } 

            return res;

        }

        /// <summary>
        /// Чтение из порта в масив байт
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            int res = 0;
            if (m_tcpclient.Connected)
            {
                int size = m_tcpclient.Available;
                if (size < count) count = size;
                if (count > 0)
                {
                    try
                    {
                        res = m_stream.Read(buffer, offset, count);
                    }
                    catch (System.IO.IOException)
                    {
                        res = -1;
                    }
                }

                else res = 0;
            }
            else res = -1;
            return res;

        }

        public int Available
        {
            get
            {
                return m_tcpclient.Available;
            }
        }



        #endregion

        #region Write
        public int Write(byte[] buffer, int offset, int count)
        {
            int res = 0;
            if (m_tcpclient.Connected)
            {
                try
                {
                    m_stream.Write(buffer, offset, count);
                    if (offset + count > buffer.Length) res = buffer.Length - offset; else res = count;
                    if (!m_tcpclient.Connected) res = -1;
                }
                catch (System.IO.IOException)
                {
                    res = -1;
                }
            }
            else res = -1;
            return res;
        }
        public int WriteByte(byte value)
        {
            int res = 0;
            byte[] buffer = new byte[1];
            buffer[0] = value;
            if (m_tcpclient.Connected)
            {
                try
                {
                    m_stream.Write(buffer, 0, 1);
                    res = 1;
                }
                catch
                {
                    res = -1;
                }
                
                if (!m_tcpclient.Connected) res = -1;
            }
            else res = -1;
            return res;
        }

        #endregion
   

    }
}
