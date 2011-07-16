using System;
using System.Threading;
using System.Collections;
using Lacross.IO;
using Lacross.IO.SerialPort;
namespace Lacross.IO.GPS
{
	/// <summary>
	/// Summary description for GPS.
	/// </summary>
	public class GPS
    {
        public const int EOF=-1;
        #region SerialPort
        public SerialPort.SerialPort m_port = null;
        public string PortName
        {
            get { return m_port.PortName; }
            set { m_port.PortName = value; }
        }
        public uint BaudRate
        {
            get { return m_port.BaudRate; }
            set { m_port.BaudRate = value; }
        }
        #endregion

        #region Property
        public bool IsRun
        {
            get
            {
                return m_port.IsOpen;
            }
        }
        #endregion

        #region Constructor & Destructor
        public GPS()
		{
            Init();
		}
        public GPS(string portName)
        {
            Init();
            m_port.PortName = portName;
        }
        public GPS(string portName, uint baudRate)
        {
            Init();
            m_port.PortName = portName;
            m_port.BaudRate = baudRate;            
        }
        private void Init()
        {
            m_port = new SerialPort.SerialPort();
            m_port.ReadTimeout = 0;
            m_port.WriteTimeout = 0;
            m_port.ReadBufferSize = 256;
            m_port.DataBits = 8;
            m_port.Parity = SerialPort.Parity.None;
            m_port.StopBits = SerialPort.StopBits.One;
            m_port.NewLine = "\n";
            m_portbuffer=new byte[m_port.ReadBufferSize];
            m_port.evDataReceived += new Lacross.IO.SerialPort.SerialDataReceivedDelegate(m_port_evDataReceived);
            
            ParserAddNMEARow(new GPGGArow());
            //ParserAddNMEARow(new GPGLLrow());
            ParserAddNMEARow(new GPGSArow());
            ParserAddNMEARow(new GPGSVrow());
            ParserAddNMEARow(new GPRMCrow());
            //ParserAddNMEARow(new GPVTGrow());
            //ParserAddNMEARow(new GPZDArow());            
        }   
        public void Close()
        {
            Stop();
            m_port.evDataReceived -= new Lacross.IO.SerialPort.SerialDataReceivedDelegate(m_port_evDataReceived);
            m_port.Close();
        }
        #endregion

        const int ST_START = 10000;
        const int ST_DOLLARGP = 10001;
        const int ST_ROW = 10002;
        const int ST_CS = 10003;
        int m_curentstate = ST_START;
        int m_currentRow = -1;
        byte[] m_portbuffer;
        NMEArow m_DollarGPxxx = new NMEArow(5);  // = GPxxx
        NMEArow m_starCS = new NMEArow(2);       // = hh
        internal Mutex m_access = new Mutex();
        public NMEArow[] RowNMEA = null;
        public int m_RMCindexRow = -1;

        #region Public Metoths
        public void Start()
        {
            m_curentstate = ST_START;
            m_DollarGPxxx.ResetValue();
            ResetResult();
            m_port.OpenPort();
        }
        public void ReStart()
        {
            if (!m_port.IsOpen) m_port.OpenPort();
            m_curentstate = ST_START;
            m_DollarGPxxx.ResetValue();
            ResetResult();
            m_port.DiscardInBuffer();
            
        }
        public void ResetResult()
        {
            m_access.WaitOne();
            m_valid = false;
            m_latitude = 0;
            m_longitude = 0;
            m_speed = 0;
            m_azimut = 0;
            m_utcdatetime = DateTime.MinValue;
            m_singnalquality = 0;
            m_satellitecount = 0;
            m_hdop = 0;
            m_altitude = 0;
            m_mode = 0;
            Array.Clear(m_currentsatelitesPRN, 0, m_currentsatelitesPRN.Length);
            m_satellites.Clear();
            m_access.ReleaseMutex();
        }
        public void Stop()
        {
            m_port.ClosePort();
        }

        public int  GetIndexOfRow(NMEArow row)
        {
            return GetIndexOfRow(row.Name);
        }
        public int  GetIndexOfRow(string name)
        {
            if (name == "") return -1;
            if (RowNMEA == null) return -1;
            if (RowNMEA.Length == 0) return -1;
            int index = -1;
            for (int c = 0; c < RowNMEA.Length; c++)
            {
                if (name == RowNMEA[c].Name)
                {
                    index = c;
                    break;
                }
            }
            return index;
        }
        public int  ParserAddNMEARow(NMEArow newrow)
        {
            
            if (m_port.IsOpen) return -1;

            string name = newrow.Name;
            if (name == "") return -1;
            newrow.Owner = this;
            if ((RowNMEA == null) || (RowNMEA.Length == 0))
            {
                RowNMEA = new NMEArow[1];
                RowNMEA[0] = newrow;
                if (name == "GPRMC") m_RMCindexRow = 0;
                return 0;
            }
            else
            {
                int index = GetIndexOfRow(name);
                if (index >=0)
                {
                    RowNMEA[index].Owner = null;
                    RowNMEA[index] = newrow;
                    return index;
                }
                else
                {
                    index = RowNMEA.Length;                    
                    NMEArow[] buf = new NMEArow[index + 1];
                    buf[index] = newrow;
                    for (int c = 0; c < RowNMEA.Length; c++)
                    {
                        buf[c] = RowNMEA[c];
                    }
                    RowNMEA = buf;
                    if (name == "GPRMC") m_RMCindexRow = index;
                    return index;
                }
            }
        }
        public bool ParserDelNMEARow(NMEArow delrow)
        {
            return ParserDelNMEARow(GetIndexOfRow(delrow.Name));            
        }
        public bool ParserDelNMEARow(string name)
        {
            return ParserDelNMEARow(GetIndexOfRow(name));           
        }
        public bool ParserDelNMEARow(int index)
        {
            if (m_port.IsOpen) return false;
            if (RowNMEA == null) return false;
            if (RowNMEA.Length == 0) return false;
            if (index >= RowNMEA.Length) return false;
            if (index < 0) return false;

            if (index == m_RMCindexRow)
            {
                m_RMCindexRow = -1;
            }
            else if (index < m_RMCindexRow)
            {
                m_RMCindexRow--;
            }

            NMEArow[] buf = new NMEArow[RowNMEA.Length - 1];
            for (int c = 0; c < index; c++) buf[c] = RowNMEA[c];
            RowNMEA[index].Owner = null;
            for (int c = index + 1; c < buf.Length; c++) buf[c] = RowNMEA[c];            
            
            RowNMEA = buf;
            return true; 

        }
        #endregion

        #region Public Property
 
        public bool Valid
        {
            get
            {
                m_access.WaitOne();
                bool r =m_valid;
                m_access.ReleaseMutex();
                return r;
            }
        }
        internal bool m_valid;
        public float Latitude
        {
            get
            {
                m_access.WaitOne();
                float r = m_latitude;
                m_access.ReleaseMutex();
                return r;
            }
        }
        internal float m_latitude;
        public float Longitude
        {
            get
            {
                m_access.WaitOne();
                float r = m_longitude;
                m_access.ReleaseMutex();
                return r;
            }
        }
        internal float m_longitude;
        public float Speed
        {
            get
            {              
                m_access.WaitOne();
                float r = m_speed;
                m_access.ReleaseMutex();
                return r;
            }
        }
        public float SpeedKmH
        {
            get
            {
                m_access.WaitOne();
                float r = m_speed;
                m_access.ReleaseMutex();
                return r/0.54f;
            }
        }
        internal float m_speed;
        public float Azimut
        {
            get
            {              
                m_access.WaitOne();
                float r = m_azimut;
                m_access.ReleaseMutex();
                return r;
            }
        }
        internal float m_azimut;
        public DateTime UTCDateTime
        {
            get
            {
                m_access.WaitOne();
                DateTime r = m_utcdatetime;
                m_access.ReleaseMutex();
                return r;
            }
        }
        internal DateTime m_utcdatetime;
        public int SingnalQuality
        {
            get
            {
                m_access.WaitOne();
                int r = m_singnalquality;
                m_access.ReleaseMutex();
                return r;
            }
        }
        internal int m_singnalquality;
        public int SatelliteCount
        {
            get
            {
                m_access.WaitOne();
                int r = m_satellitecount;
                m_access.ReleaseMutex();
                return r;
            }
        }
        internal int m_satellitecount;
        public float HDOP
        {
            get
            {
                m_access.WaitOne();
                float r = m_hdop;
                m_access.ReleaseMutex();
                return r;
            }
        }
        internal float m_hdop;
        public float Altitude
        {
            get
            {
                m_access.WaitOne();
                float r = m_altitude;
                m_access.ReleaseMutex();
                return r;
            }
        }
        internal float m_altitude;
        public int GPSMode
        {
            get
            {
                m_access.WaitOne();
                int r = m_mode;
                m_access.ReleaseMutex();
                return r;
            }
        }
        internal int m_mode;
        public int[] CurrentSatelitesPRN
        {
            get
            {
                int[] r = new int[m_currentsatelitesPRN.Length];
                m_access.WaitOne();
                Array.Copy(m_currentsatelitesPRN, 0,r, 0,r.Length);                
                m_access.ReleaseMutex();
                return r;
            }
        }
        internal int[] m_currentsatelitesPRN = new int[12];
        public SatelliteInfo GetSatellite(int prn)
        {
            SatelliteInfo res = null;
            object src = null;
            m_access.WaitOne();
            src = m_satellites[prn];
            if (src != null)
            {
                res = new SatelliteInfo(src as SatelliteInfo);
            }
            else res =new SatelliteInfo();
            src = null;            
            m_access.ReleaseMutex();
            return res;
        }
        internal Hashtable m_satellites = new Hashtable(36);

        public SatelliteInfo[] GetSatellites()
        {
            SatelliteInfo[] res = new SatelliteInfo[12];
            object src = null;
            
            m_access.WaitOne();
            for (int i = 0; i < 12; i++)
            {
                if (m_currentsatelitesPRN[i] == 0) break;
                src = m_satellites[m_currentsatelitesPRN[i]];
                if (src != null)
                {
                    res[i] = new SatelliteInfo(src as SatelliteInfo);
                } 
            }            
            src = null;
            m_access.ReleaseMutex();

            return res;
        }
        #endregion

        #region Events
        public event GPSSerialDataReceivedDelegate evSerialDataReceived;
        public event GPSDataReceivedDelegate evDataReceived;
        public event GPSAllDataParsedDelegate evAllDataParsed;
        void OnSerialDataReceived(SerialData e,int count)
        {
            if (!IsRun) return;
            if (evSerialDataReceived != null)
            {
                string txt = null;
                if (e == SerialData.Chars)
                {
                    if (count > 0)
                    {
                        txt = System.Text.Encoding.Default.GetString(m_portbuffer, 0, count);
                        evSerialDataReceived(this, e, txt);
                    }
                }
                else
                {
                  evSerialDataReceived(this, e, txt);
                }
            }
        }
        void OnDataReceived(int indexrow)
        {
            if (!IsRun) return;
            if (indexrow == m_RMCindexRow)
            {
                if (evAllDataParsed != null)
                {
                    evAllDataParsed(this);
                }
            }
            if (evDataReceived != null)
            {
                evDataReceived(this, indexrow);
            }
        }
        #endregion
     
        #region EventThread
        void m_port_evDataReceived(object sender, Lacross.IO.SerialPort.SerialData e)
        {
            if (e == Lacross.IO.SerialPort.SerialData.Eof)
            {
                OnSerialDataReceived(SerialData.Eof,0);
                OnDataReceived(EOF);
                return;
            }

            byte curByte = 0;
            int count = 0;


            while ((count = m_port.Read(m_portbuffer, 0, m_portbuffer.Length))>0)
            {
                OnSerialDataReceived(SerialData.Chars,count);

                for (int i = 0; i < count; i++)
                {
                    curByte = m_portbuffer[i];

                    switch (m_curentstate)
                    {
                        case ST_START:
                            if (curByte == (byte)NMEAChars.Dollar)
                            {
                                m_DollarGPxxx.ResetValue();
                                m_curentstate = ST_DOLLARGP;
                            }
                            break;
                        case ST_DOLLARGP:
                            if (!m_DollarGPxxx.AddByte(curByte))
                            {
                                string nnn = m_DollarGPxxx.ToString();
                                m_currentRow = -1;
                                m_curentstate = ST_START;
                                for (int c = 0; c < RowNMEA.Length; c++)
                                {
                                    if (nnn == RowNMEA[c].Name)
                                    {
                                        m_currentRow = c;
                                        RowNMEA[c].ResetValue();
                                        RowNMEA[c].AddByte(curByte);
                                        m_curentstate = ST_ROW;
                                        break;
                                    }
                                }

                            }
                            break;
                        case ST_ROW:
                            if (curByte != (byte)NMEAChars.Star)
                            {
                                RowNMEA[m_currentRow].AddByte(curByte);
                            }
                            else
                            {
                                m_curentstate = ST_CS;
                                m_starCS.ResetValue();
                            }
                            break;
                        case ST_CS:
                            if (!m_starCS.AddByte(curByte))
                            {
                                m_curentstate = ST_START;
                                byte cs = byte.Parse(m_starCS.ToString(), System.Globalization.NumberStyles.HexNumber);
                                if (RowNMEA[m_currentRow].CheckCS(cs))
                                {
                                    OnDataReceived(m_currentRow);
                                }
                            }
                            break;
                    }
                }
            }
        }
        #endregion

    }
}
