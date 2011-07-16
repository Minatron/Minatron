using System;
using Lacross.OS;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;

namespace Lacross.IO.SerialPort
{
    public class SerialPort
    {
        #region Const
        public const uint InfiniteTimeout = 500;
        #endregion  

        #region Public Property
        /// <summary>
        /// true если порт открыт
        /// </summary>
        public bool IsOpen 
        { 
            get{ return m_IsOpen;} 
        }
        bool m_IsOpen=false;
        
        /// <summary>
        /// Имя последовательного порта
        /// </summary>
        public string PortName 
        { 
            get{ return m_PortName;}
            set{ m_PortName=value;}
        }
        string m_PortName="COM1";

        /// <summary>
        /// Скорость обмена данными через порт (bps)
        /// </summary>
        public uint BaudRate 
        { 
            get{ return m_BaudRate;} 
            set{ m_BaudRate=value;}
        }
        uint m_BaudRate=9600;

        /// <summary>
        /// Количество бит на байт (5,6,7,8)
        /// </summary>
        public byte DataBits 
        { 
            get{ return m_DataBits;} 
            set{ m_DataBits=value;} 
        }
        byte m_DataBits=8;

        /// <summary>
        /// Контроль четности байта
        /// </summary>
        public Parity Parity 
        { 
            get{ return m_Parity;}
            set{ m_Parity =value;} 
        }
        Parity m_Parity=Parity.None;

        /// <summary>
        /// количество Stop битов
        /// </summary>
        public StopBits StopBits
        {
            get { return m_StopBits; }
            set { m_StopBits = value; }
        }
        StopBits m_StopBits = StopBits.One;
        
        /// <summary>
        /// размер входящего буфера в байтах
        /// </summary>
        public int ReadBufferSize
        {
            get { return m_ReadBufferSize; }
            set { m_ReadBufferSize = value; }
        }
        int m_ReadBufferSize = 1024;       

        /// <summary>
        /// размер исходящего буфера в байтах
        /// </summary>
        public int WriteBufferSize
        {
            get { return m_WriteBufferSize; }
            set { m_WriteBufferSize = value; }
        }
        int m_WriteBufferSize = 1024;

        /// <summary>
        /// Символ новой строки
        /// </summary>
        public string NewLine 
        { 
            get{ return m_NewLine;}
            set{ m_NewLine=value;} 
        }
        string m_NewLine="\n";

        /// <summary>
        /// Включает игнорирование нулевых байтов при приеме. 
        /// Если это поле true, нулевые байты будут игнорироваться при получении.
        /// </summary>
        public bool DiscardNull
        {
            get { return m_DiscardNull; }
            set { m_DiscardNull = value; }
        }
        bool m_DiscardNull = false;

        /// <summary>
        /// Задает байт-замену для ошибочных байтов
        /// </summary>
        public byte ParityReplace
        {
            get { return m_ParityReplace; }
            set { m_ParityReplace = value; }
        }
        byte m_ParityReplace = 0;

        /// <summary>
        /// true if the port is in a break state; otherwise, false. 
        /// </summary>
        public bool BreakState
        {
            get
            {
                if (!m_IsOpen) return false;
                return (m_BreakState == 1);
            }
            set
            {
                if (!m_IsOpen) return;
                if (m_BreakState < 0) return;
                if (m_hPort == (IntPtr)WinBase.INVALID_HANDLE_VALUE) return;
                if (value)
                {
                    WinBase.EscapeCommFunction(m_hPort, (uint)WinBase.CommEscapes.SETBREAK);
                    m_BreakState = 1;
                }
                else
                {
                    WinBase.EscapeCommFunction(m_hPort, (uint)WinBase.CommEscapes.CLRBREAK);
                    m_BreakState = 0;
                }
            }
        }
        int m_BreakState = 0;

        /// <summary>
        /// The number of milliseconds before a time-out occurs when a read operation does not finish. 
        /// </summary>
        public uint ReadTimeout
        {
            get { return m_ReadTimeout; }
            set { m_ReadTimeout = value; }
        }
        uint m_ReadTimeout = InfiniteTimeout;

        /// <summary>
        /// The number of milliseconds before a time-out occurs. The default is InfiniteTimeout. 
        /// </summary>
        public uint WriteTimeout
        {
            get { return m_WriteTimeout; }
            set { m_WriteTimeout = value; }
        }
        uint m_WriteTimeout = InfiniteTimeout;
   
        #endregion

        #region Private Fields      
        private IntPtr m_closeEvent;
        private IntPtr m_hPort = (IntPtr)WinBase.INVALID_HANDLE_VALUE;
        private Thread m_eventThread;
        private ManualResetEvent m_threadStarted = new ManualResetEvent(false);
        private Mutex m_ReadBufferBusy = new Mutex();
        #endregion

        #region Construction
        public SerialPort()
        {
            Init();
        }
        public SerialPort(string portName)
        {
            m_PortName = portName;
            Init();
        }
        public SerialPort(string portName, uint baudRate)
        {
            m_PortName = portName;
            m_BaudRate = baudRate;
            Init();
        }
        public SerialPort(string portName, uint baudRate, Parity parity)
        {
            m_PortName = portName;
            m_BaudRate = baudRate;
            m_Parity = parity;
            Init();
        }
        public SerialPort(string portName, uint baudRate, Parity parity, byte dataBits)
        {
            m_PortName = portName;
            m_BaudRate = baudRate;
            m_Parity = parity;
            m_DataBits = dataBits;
            Init();
        }
        public SerialPort(string portName, uint baudRate, Parity parity, byte dataBits, StopBits stopBits)
        {           
            m_PortName = portName;
            m_BaudRate = baudRate;
            m_Parity = parity;
            m_DataBits = dataBits;
            m_StopBits = stopBits;
            Init();
        }
        void Init()
        {       
            m_closeEvent = WinBase.CreateEvent(IntPtr.Zero, Convert.ToInt32(true), Convert.ToInt32(false), "CloseEvent");
        }
        public void Close()
        {
            //m_ReadBufferBusy.ReleaseMutex();
            ClosePort();
          
        }
        #endregion

        #region Events
        public event SerialDataReceivedDelegate     evDataReceived;
        void OnDataReceived(SerialData e)
        {
            try
            {
                if (evDataReceived != null)
                {
                    evDataReceived(this, e);
                }
            }
            catch (ObjectDisposedException)
            {                
            }
        }
        public event SerialErrorReceivedDelegate    evErrorReceived;
        void OnErrorReceived(SerialError e)
        {
            try
            {
                if (evErrorReceived != null)
                {
                    evErrorReceived(this, e);
                }
            }
            catch (ObjectDisposedException)
            {
            }
        }
        public event SerialPinChangedDelegate       evPinChanged;
        void OnPinChanged(SerialPinChange e)
        {   
            try
            {
                if (evPinChanged != null)
                {
                    evPinChanged(this, e);
                }
            }
            catch (ObjectDisposedException)
            {
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Открыть последовательный порт
        /// </summary>
        public void OpenPort()
        {
            if (m_IsOpen) return;
            m_hPort = WinBase.CreateFile(m_PortName+":", WinBase.GENERIC_READ | WinBase.GENERIC_WRITE, 0, IntPtr.Zero, WinBase.OPEN_EXISTING, 0, IntPtr.Zero);
            if (m_hPort == (IntPtr)WinBase.INVALID_HANDLE_VALUE)
            {
                int e = Marshal.GetLastWin32Error();

                if (e != (int)WinBase.APIErrors.ERROR_ACCESS_DENIED)
                {
                    throw new SerialPortException("CreateFile Failed: "+e.ToString());
                }

                return;
            }
            OpenPort(m_hPort);           
        }

        /// <summary>
        /// Открыть последовательный порт по handel 
        /// </summary>
        public void OpenPort(IntPtr handle)
        {
            if (m_IsOpen) return;
            m_hPort = handle;

            m_IsOpen = true;

            Update();
            WinBase.CommErrorFlags errFlags = new WinBase.CommErrorFlags();
            WinBase.ClearCommError(m_hPort, ref errFlags, IntPtr.Zero);
            DiscardInBuffer();
            m_eventThread = new Thread(new ThreadStart(CommEventThread));
            m_eventThread.Priority = ThreadPriority.Highest;
            m_eventThread.Start();
            
            m_threadStarted.WaitOne();
        }

        public void Update()
        {
            if (!m_IsOpen) return;

            // завести память под буферы 

           WinBase.SetupComm(m_hPort, m_ReadBufferSize, m_WriteBufferSize);


            WinBase.DCB dcb=new WinBase.DCB();
            dcb.DCBlength = Convert.ToUInt32(Marshal.SizeOf(typeof(WinBase.DCB)));
            dcb.BaudRate = m_BaudRate;

            dcb.fBinary = 1;  // всегда TRUE
            dcb.fParity = (m_Parity == Parity.None) ? (uint)0 : (uint)1;
            dcb.fOutxCtsFlow = 0;
            dcb.fOutxDsrFlow = 0;
            dcb.fDtrControl = WinBase.DTR_CONTROL_DISABLE;
            dcb.fDsrSensitivity = 0;
            dcb.fTXContinueOnXoff = 1;
            dcb.fOutX = 0;
            dcb.fInX = 0;
            dcb.fErrorChar = 1; // разрешает замещение ошибочных байтов
            dcb.fNull = Convert.ToUInt32(m_DiscardNull);
            dcb.fRtsControl = WinBase.RTS_CONTROL_DISABLE;
            dcb.fAbortOnError = 0;

            dcb.XonLim = (ushort)(m_ReadBufferSize / 10);
            dcb.XoffLim = (ushort)(m_ReadBufferSize / 10);

            dcb.byteSize = m_DataBits;
            dcb.Parity = (byte)m_Parity;
            dcb.StopBits = (byte)m_StopBits;

            dcb.XonChar = 0x11;
            dcb.XoffChar = 0x13;

            dcb.ErrorChar = m_ParityReplace;

            dcb.EofChar = 0x04;

            dcb.EvtChar = (byte)m_NewLine[0];

            WinBase.SetCommState(m_hPort, ref dcb);

            WinBase.COMMTIMEOUTS ct=new WinBase.COMMTIMEOUTS();
            ct.ReadIntervalTimeout = uint.MaxValue;
            ct.ReadTotalTimeoutMultiplier =  uint.MaxValue;
            ct.ReadTotalTimeoutConstant = m_ReadTimeout;
            ct.WriteTotalTimeoutMultiplier = 0;
            ct.WriteTotalTimeoutConstant = m_WriteTimeout;

            WinBase.SetCommTimeouts(m_hPort, ref ct);           
        }

        /// <summary>
        /// Закрыть последовательный порт
        /// </summary>
        public void ClosePort()
        {
            if (!m_IsOpen) return;
            
            m_IsOpen = false;
            if (WinBase.CloseHandle(m_hPort))
            {
                WinBase.SetEvent(m_closeEvent);
                m_IsOpen = false;
                m_hPort = (IntPtr)WinBase.INVALID_HANDLE_VALUE;
                WinBase.SetEvent(m_closeEvent);
            }
            //m_eventThread.Abort();
        }

        /// <summary>
        /// Список последовательных портов
        /// </summary>
        /// <returns></returns>
        public static string[] GetPortNames()
        {
            string[] res=new string[9];
            res[0] = "COM1";
            res[1] = "COM2";
            res[2] = "COM3";
            res[3] = "COM4";
            res[4] = "COM5";
            res[5] = "COM6";
            res[6] = "COM7";
            res[7] = "COM8";
            res[8] = "COM9";
            return res;
        }

        /// <summary>
        /// Отбросить входящие данные из последовательного порта
        /// </summary>
        public void DiscardInBuffer()
        {
            if (!m_IsOpen) return;
            m_ReadBufferBusy.WaitOne();
            WinBase.PurgeComm(m_hPort, WinBase.PURGE_RXCLEAR | WinBase.PURGE_TXABORT);
            m_ReadBufferBusy.ReleaseMutex();
        }

        /// <summary>
        /// Отбросить исходящие данные из последовательного порта
        /// </summary>
        public void DiscardOutBuffer()
        {
            if (!m_IsOpen) return;
            WinBase.PurgeComm(m_hPort, WinBase.PURGE_TXCLEAR | WinBase.PURGE_RXABORT);
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
            if (!m_IsOpen) return 0;
            if (m_hPort == (IntPtr)WinBase.INVALID_HANDLE_VALUE) return 0;            
            int res = 0;
            m_ReadBufferBusy.WaitOne();
            if (WinBase.ReadFile(m_hPort, buffer, count, out res, IntPtr.Zero) == 0)
            {
                    res=0;
            }
            m_ReadBufferBusy.ReleaseMutex();            
            return res;
        }
        
        /// <summary>
        /// Чтение из порта в строку
        /// </summary>
        /// <returns></returns>
        public string ReadExisting()
        {
            byte[] buffer = new byte[m_ReadBufferSize];
            int res=Read(buffer, 0, buffer.Length);
            return System.Text.Encoding.Default.GetString(buffer, 0, res);
        }

        /// <summary>
        /// запись в порт с переводом строки
        /// </summary>
        /// <param name="text"></param>
        public void WriteLine(string text)
        {
            Write(text + m_NewLine);
        }
       
        /// <summary>
        /// запись в порт 
        /// </summary>
        /// <param name="text"></param>
        public void Write(string text)
        {
            if (text.Length == 0) return;
            byte[] buffer = System.Text.Encoding.Default.GetBytes(text);
            Write(buffer, 0, buffer.Length);                
        }
        
        /// <summary>
        /// запись в порт
        /// </summary>
        /// <param name="buffer">байтовый масив</param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public void Write(byte[] buffer, int offset, int count)
        {
            if (!m_IsOpen) return;
            int n=0;
            WinBase.WriteFile(m_hPort,buffer,buffer.Length,out n,IntPtr.Zero);          
        }
        #endregion

        #region EventThread
        private void CommEventThread()
        {
            WinBase.CommEventFlags eventFlags = new WinBase.CommEventFlags();
            AutoResetEvent rxevent = new AutoResetEvent(false);

            WinBase.SetCommMask(m_hPort, WinBase.CommEventFlags.ALL);

            try
            {
                // сообщить о запуске в PortOpen()
                m_threadStarted.Set();
                #region Thread loop
                while (m_hPort != (IntPtr)WinBase.INVALID_HANDLE_VALUE)
                {
                    // ждем событие от последовательного порта
                    if (WinBase.WaitCommEvent(m_hPort, ref eventFlags, IntPtr.Zero)==0 )
                    {
                        #region Не ДОЖДАЛИСЬ !!!
                   
                        int e = Marshal.GetLastWin32Error();
                 
                        if (e == (int)WinBase.APIErrors.ERROR_IO_PENDING)
                        {
                            // дурацкая ошибка, подождем и попробуем опять 
                            rxevent.WaitOne();
                            Thread.Sleep(0);
                            continue;
                        }

                        if (e == (int)WinBase.APIErrors.ERROR_INVALID_HANDLE)
                        {
                            // Ага !! произошел вызов ClosePort() 
                            // Thread.Abort() - в CE остутствует вот и приходиться извращаться, иначе глюк - зависание

                            // подождем 1 секунду сигнала  m_closeEvent
                            int eventResult = WinBase.WaitForSingleObject(m_closeEvent, 1000);

                            if (eventResult == (int)WinBase.APIConstants.WAIT_OBJECT_0)
                            {   // успешно подождали

                                m_hPort = (IntPtr)WinBase.INVALID_HANDLE_VALUE;

                                m_threadStarted.Reset();

                                if (m_IsOpen)
                                {
                                    throw new SerialPortException("Whait Failed: "+e.ToString());  
                                } 
                                return;
                            }
                        }

                        if (e == 995)
                        {
                            // завершаем поток :)))
                            return;
                        }
                        else
                        {
                            throw new SerialPortException("Whait Failed: " + e.ToString());
                        }
                        #endregion
                    }

                    // пере устанавливаем ожидания событий от порта
                    WinBase.SetCommMask(m_hPort, WinBase.CommEventFlags.ALL);

                    if ((eventFlags & WinBase.CommEventFlags.ERR) !=0)
                    {
                        
                        #region ПРИШЛА ОШИБКА
                        WinBase.CommErrorFlags errFlags = new WinBase.CommErrorFlags();
                        if (WinBase.ClearCommError(m_hPort, ref errFlags, IntPtr.Zero)==0)
                        {
                            throw new SerialPortException("ClearCommError Failed: "+Marshal.GetLastWin32Error().ToString());
                        }
                        if ((errFlags & WinBase.CommErrorFlags.BREAK) != 0)
                        {
                            eventFlags |= WinBase.CommEventFlags.BREAK;
                        }
                        else 
                        {
                            if ((errFlags & WinBase.CommErrorFlags.FRAME) != 0) OnErrorReceived(SerialError.Frame);
                            if ((errFlags & WinBase.CommErrorFlags.IOE) != 0) OnErrorReceived(SerialError.IOE);
                            if ((errFlags & WinBase.CommErrorFlags.MODE) != 0) OnErrorReceived(SerialError.Mode);
                            if ((errFlags & WinBase.CommErrorFlags.OVERRUN) != 0) OnErrorReceived(SerialError.Overrun);                            
                            if ((errFlags & WinBase.CommErrorFlags.RXOVER) != 0) OnErrorReceived(SerialError.RXOver);                            
                            if ((errFlags & WinBase.CommErrorFlags.RXPARITY) != 0) OnErrorReceived(SerialError.RXParity);
                            if ((errFlags & WinBase.CommErrorFlags.TXFULL) != 0) OnErrorReceived(SerialError.TXFull);                            
                            //continue;
                        }

                        #endregion
                    }

                    if ((eventFlags & WinBase.CommEventFlags.RXCHAR) == WinBase.CommEventFlags.RXCHAR)
                    {
                        #region ПРИШЛИ ДАННЫЕ
                        OnDataReceived(SerialData.Chars);   
                        
                        #endregion
                    }

                    if ((eventFlags & WinBase.CommEventFlags.NONE) == WinBase.CommEventFlags.NONE)
                    {
                        #region ПРИШЛИ ДАННЫЕ

                         OnDataReceived(SerialData.Chars);

                        #endregion
                    }
                }
                #endregion
            }
            catch 
            {             
                OnDataReceived(SerialData.Eof);                  
            }


        }
        #endregion
    }
}
