using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Band.WeightData.Terminal
{
    public class Terminal
    {
        const int BUFFERSIZE = 1024;

        readonly NetworkStream _stream;
        readonly TcpClient     _connect;
        readonly byte[]         _buffer;
        bool _isWork = false;

        public Terminal(TcpClient connect)
        {
            _connect = connect;
            _stream = connect.GetStream();
            _buffer = new byte[BUFFERSIZE];
            _isWork = true;
        }

        public bool IsConnect
        {
            get 
            {
                if (_isWork)
                {
                    return _connect.Connected;
                }
                return _isWork; 
            }
        }

        public void Close()
        {
            if (_isWork)
            {
                _stream.Close();
                _connect.Close();
                _isWork = false;
            }
        }

        public string[] ReadPackets()
        {
            int size = _connect.Available;
            if (size > 0 )
            {
                try
                {
                    size = _stream.Read(_buffer, 0, (size > BUFFERSIZE) ? BUFFERSIZE : size);
                    return Encoding.ASCII.GetString(_buffer, 0, size).Split(new string[] { "\r\n", "\n", ")" }, StringSplitOptions.RemoveEmptyEntries);
                }
                catch  {}
            }
            return null;
        }

        public void Write(string packet)
        {
            int size = packet.Length;
            size = Encoding.ASCII.GetBytes(packet, 0, (size > BUFFERSIZE) ? BUFFERSIZE : size, _buffer, 0);
            _stream.Write(_buffer, 0, size);
        }

        static int count = 0;
        public static void DoWork(object trm)
        {
            Terminal terminal = trm as Terminal;
            count++;
            Thread.CurrentThread.Name = "TERMINAL_"+count.ToString();

            var db = new DataBase();
            while (terminal.IsConnect && db.IsConnect)
            {
                string[] pakets = terminal.ReadPackets();
                if (pakets != null )
                {
                    foreach (string packet in pakets)
                    {
                        db.Store(packet.Trim());
                    }
                    terminal.Write("Connection OK\r\n");
                    break;
                }
                Thread.Sleep(100);
            }

            db.Close();
            terminal.Close();
        }
    }
}
