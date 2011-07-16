using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Lacross.OS
{
    public sealed class WinBase
    {
        #region CONSTANTS

        public enum WM : int
        {
            CREATE = 0x1,
            DESTROY = 0x2,
            MOVE = 0x3,
            SIZE = 0x5,
            ACTIVATE = 0x6,
            SETFOCUS = 0x7,
            KILLFOCUS = 0x8,
            SETREDRAW = 0xB,
            SETTEXT = 0xC,
            GETTEXT = 0xD,
            GETTEXTLENGTH = 0xE,
            PAINT = 0xF,
            CLOSE = 0x0010,
            QUIT = 0x0012,
            ERASEBKGND = 0x0014,
            SYSCOLORCHANGE = 0x0015,
            SHOWWINDOW = 0x0018,
            WININICHANGE = 0x001A,
            SETTINGCHANGE = WININICHANGE,
            FONTCHANGE = 0x001D,
            CANCELMODE = 0x001F,
            SETCURSOR = 0x20,
            NEXTDLGCTL = 0x28,
            DRAWITEM = 0x2B,
            MEASUREITEM = 0x002C,
            DELETEITEM = 0x002D,
            SETFONT = 0x0030,
            GETFONT = 0x0031,
            COMPAREITEM = 0x0039,
            WINDOWPOSCHANGED = 0x0047,
            NOTIFY = 0x004E,
            HELP = 0x0053,
            STYLECHANGED = 0x007D,
            GETDLGCODE = 0x0087,
            KEYFIRST = 0x0100,
            KEYDOWN = 0x0100,
            KEYUP = 0x0101,
            CHAR = 0x0102,
            DEADCHAR = 0x0103,
            SYSKEYDOWN = 0x0104,
            SYSKEYUP = 0x0105,
            SYSCHAR = 0x0106,
            SYSDEADCHAR = 0x0107,
            KEYLAST = 0x0108,
            IM_INFO = 0x010C,
            IME_STARTCOMPOSITION = 0x010D,
            IME_ENDCOMPOSITION = 0x010E,
            IME_COMPOSITION = 0x010F,
            IME_KEYLAST = 0x010F,

            IME_SETCONTEXT = 0x0281,
            IME_NOTIFY = 0x0282,
            IME_CONTROL = 0x0283,
            IME_COMPOSITIONFULL = 0x0284,
            IME_SELECT = 0x0285,
            IME_CHAR = 0x0286,
            IME_SYSTEM = 0x0287,
            IME_REQUEST = 0x0288,
            IME_KEYDOWN = 0x0290,
            IME_KEYUP = 0x0291,
            INITDIALOG = 0x0110,
            COMMAND = 0x0111,
            SYSCOMMAND = 0x0112,
            TIMER = 0x0113,
            HSCROLL = 0x0114,
            VSCROLL = 0x0115,
            INITMENUPOPUP = 0x0117,
            MENUCHAR = 0x0120,
            MOUSEFIRST = 0x0200,
            MOUSEMOVE = 0x0200,
            /// <summary>   
            /// This message is posted when the user presses the touch-screen in the client area of a window.   
            /// </summary>      
            LBUTTONDOWN = 0x0201,
            LBUTTONUP = 0x0202,
            LBUTTONDBLCLK = 0x0203,
            RBUTTONDOWN = 0x0204,
            RBUTTONUP = 0x0205,
            RBUTTONDBLCLK = 0x0206,
            MBUTTONDOWN = 0x0207,
            MBUTTONUP = 0x0208,
            MBUTTONDBLCLK = 0x0209,
            MOUSEWHEEL = 0x020A,
            MOUSELAST = 0x020A,
            ENTERMENULOOP = 0x0211,
            EXITMENULOOP = 0x0212,
            CAPTURECHANGED = 0x0215,
            CUT = 0x0300,
            COPY = 0x0301,
            PASTE = 0x0302,
            CLEAR = 0x0303,
            UNDO = 0x0304,
            RENDERFORMAT = 0x0305,
            RENDERALLFORMATS = 0x0306,
            DESTROYCLIPBOARD = 0x0307,
            QUERYNEWPALETTE = 0x030F,
            PALETTECHANGED = 0x0311,
            CTLCOLORMSGBOX = 0x0132,
            CTLCOLOREDIT = 0x0133,
            CTLCOLORLISTBOX = 0x0134,
            CTLCOLORBTN = 0x0135,
            CTLCOLORDLG = 0x0136,
            CTLCOLORSCROLLBAR = 0x0137,
            CTLCOLORSTATIC = 0x0138,
            VKEYTOITEM = 0x002E,
            CHARTOITEM = 0x002F,
            QUERYDRAGICON = 0x0037,
            DBNOTIFICATION = 0x03FD,
            NETCONNECT = 0x03FE,
            HIBERNATE = 0x03FF,
            /// <summary>   
            /// This message is used by applications to help define private messages.   
            /// </summary> 
            USER = 0x0400,
            APP = 0x8000,
        }


        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMAXIMIZED = 3;

        /// <summary>
        /// Распределяет фиксированную память. Возращает указатель на объект памяти.
        /// </summary>
        public const uint LMEM_FIXED = 0;

        /// <summary>
        /// Объединяет флаги LMEM_FIXED и LMEM_ZEROINIT
        /// </summary>
        public const uint LMEM_MOVEABLE = 2;

        /// <summary>
        /// Инициализирует содержание памяти в нуль.
        /// </summary>
        public const uint LMEM_ZEROINIT = 0x0040;

        public const Int32 INVALID_HANDLE_VALUE = -1;
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint NOSHARE = 0x00000000;
        public const uint SHARE_READ = 0x00000001;
        public const uint SHARE_WRITE = 0x00000002;
        public const uint OPEN_EXISTING = 3;

        public const uint PAGE_READONLY = 0x02;
        public const uint PAGE_READWRITE = 0x04;
        public const uint FILE_MAP_READ = 0x0004;
        public const uint FILE_MAP_ALL_ACCESS = 0xF001F;

        public const uint DTR_CONTROL_DISABLE = 0x00;
        public const uint DTR_CONTROL_ENABLE = 0x01;
        public const uint DTR_CONTROL_HANDSHAKE = 0x02;
        public const uint RTS_CONTROL_DISABLE = 0x00;
        public const uint RTS_CONTROL_ENABLE = 0x01;
        public const uint RTS_CONTROL_HANDSHAKE = 0x02;
        public const uint RTS_CONTROL_TOGGLE = 0x03;

        public const uint EVENT_PULSE = 1;
        public const uint EVENT_RESET = 2;
        public const uint EVENT_SET = 3;

        public const int PURGE_RXABORT = 0x2;
        public const int PURGE_RXCLEAR = 0x8;
        public const int PURGE_TXABORT = 0x1;
        public const int PURGE_TXCLEAR = 0x4;

        public enum APIErrors : int
        {
            /// <summary>
            /// Port not found
            /// </summary>
            ERROR_FILE_NOT_FOUND = 2,
            /// <summary>
            /// Invalid port name
            /// </summary>
            ERROR_INVALID_NAME = 123,
            /// <summary>
            /// Access denied
            /// </summary>
            ERROR_ACCESS_DENIED = 5,
            /// <summary>
            /// invalid handle
            /// </summary>
            ERROR_INVALID_HANDLE = 6,
            /// <summary>
            /// IO pending
            /// </summary>
            ERROR_IO_PENDING = 997
        }

        public enum APIConstants : uint
        {
            WAIT_OBJECT_0 = 0x00000000,
            WAIT_ABANDONED = 0x00000080,
            WAIT_ABANDONED_0 = 0x00000080,
            WAIT_FAILED = 0xffffffff,
            WAIT_TIMEOUT = 0x00000102,
            INFINITE = 0xffffffff,
            ERROR_ALREADY_EXISTS = 0x000000B7
        }

        [Flags]
        public enum CommEventFlags : int
        {
            /// <summary>
            /// No flags
            /// </summary>
            NONE = 0x0000, //
            /// <summary>
            /// Event on receive
            /// </summary>
            RXCHAR = 0x0001, // Any Character received
            /// <summary>
            /// Event when specific character is received
            /// </summary>
            RXFLAG = 0x0002, // Received specified flag character
            /// <summary>
            /// Event when the transmit buffer is empty
            /// </summary>
            TXEMPTY = 0x0004, // Tx buffer Empty
            /// <summary>
            /// Event on CTS state change
            /// </summary>
            CTS = 0x0008, // CTS changed
            /// <summary>
            /// Event on DSR state change
            /// </summary>
            DSR = 0x0010, // DSR changed
            /// <summary>
            /// Event on RLSD state change
            /// </summary>
            RLSD = 0x0020, // RLSD changed
            /// <summary>
            /// Event on BREAK
            /// </summary>
            BREAK = 0x0040, // BREAK received
            /// <summary>
            /// Event on line error
            /// </summary>
            ERR = 0x0080, // Line status error
            /// <summary>
            /// Event on ring detect
            /// </summary>
            RING = 0x0100, // ring detected
            /// <summary>
            /// Event on printer error
            /// </summary>
            PERR = 0x0200, // printer error
            /// <summary>
            /// Event on 80% high-water
            /// </summary>
            RX80FULL = 0x0400, // rx buffer is at 80%
            /// <summary>
            /// Provider event 1
            /// </summary>
            EVENT1 = 0x0800, // provider event
            /// <summary>
            /// Provider event 2
            /// </summary>
            EVENT2 = 0x1000, // provider event
            /// <summary>
            /// Event on CE power notification
            /// </summary>
            POWER = 0x2000, // wince power notification

            //#if WINCE
            //ALL = 0x3FFF
            //#endif
            //#if WIN32
            ALL = BREAK | CTS | DSR | ERR | RING | RLSD | RXCHAR | RXFLAG | TXEMPTY
            // #endif            
        }

        public enum CommEscapes : uint
        {
            /// <summary>
            /// Causes transmission to act as if an XOFF character has been received.
            /// </summary>
            SETXOFF = 1,
            /// <summary>
            /// Causes transmission to act as if an XON character has been received.
            /// </summary>
            SETXON = 2,
            /// <summary>
            /// Sends the RTS (Request To Send) signal.
            /// </summary>
            SETRTS = 3,
            /// <summary>
            /// Clears the RTS (Request To Send) signal
            /// </summary>
            CLRRTS = 4,
            /// <summary>
            /// Sends the DTR (Data Terminal Ready) signal.
            /// </summary>
            SETDTR = 5,
            /// <summary>
            /// Clears the DTR (Data Terminal Ready) signal.
            /// </summary>
            CLRDTR = 6,
            /// <summary>
            /// Suspends character transmission and places the transmission line in a break state until the ClearCommBreak function is called (or EscapeCommFunction is called with the CLRBREAK extended function code). The SETBREAK extended function code is identical to the SetCommBreak function. This extended function does not flush data that has not been transmitted.
            /// </summary>
            SETBREAK = 8,
            /// <summary>
            /// Restores character transmission and places the transmission line in a nonbreak state. The CLRBREAK extended function code is identical to the ClearCommBreak function
            /// </summary>
            CLRBREAK = 9,
            ///Set the port to IR mode.
            SETIR = 10,
            /// <summary>
            /// Set the port to non-IR mode.
            /// </summary>
            CLRIR = 11
        }

        [Flags]
        public enum CommErrorFlags : int
        {
            /// <summary>
            /// Receive overrun
            /// </summary>
            RXOVER = 0x0001,
            /// <summary>
            /// Overrun
            /// </summary>
            OVERRUN = 0x0002,
            /// <summary>
            /// Parity error
            /// </summary>
            RXPARITY = 0x0004,
            /// <summary>
            /// Frame error
            /// </summary>
            FRAME = 0x0008,
            /// <summary>
            /// BREAK received
            /// </summary>
            BREAK = 0x0010,
            /// <summary>
            /// Transmit buffer full
            /// </summary>
            TXFULL = 0x0100,
            /// <summary>
            /// IO Error
            /// </summary>
            IOE = 0x0400,
            /// <summary>
            /// Requested mode not supported
            /// </summary>
            MODE = 0x8000
        }

        #endregion

        #region STRUCTURES

        [StructLayout(LayoutKind.Sequential)]
        public struct DCB
        {
            public UInt32 DCBlength;
            public UInt32 BaudRate;
            public UInt32 control;

            public uint fBinary
            {
                set { control = (control & 0xFFFFFFFE) | (value & 0x00000001); }
                get { return control & 0x00000001; }
            }

            public uint fParity
            {
                set { control = (control & 0xFFFFFFFD) | ((value & 0x00000001) << 1); }
                get { return (control >> 1) & 0x00000001; }
            }

            public uint fOutxCtsFlow
            {
                set { control = (control & 0xFFFFFFFB) | ((value & 0x00000001) << 2); }
                get { return (control >> 2) & 0x00000001; }
            }

            public uint fOutxDsrFlow
            {
                set { control = (control & 0xFFFFFFF7) | ((value & 0x00000001) << 3); }
                get { return (control >> 3) & 0x00000001; }
            }

            public uint fDtrControl
            {
                set { control = (control & 0xFFFFFFCF) | ((value & 0x00000003) << 4); }
                get { return (control >> 4) & 0x00000003; }
            }

            public uint fDsrSensitivity
            {
                set { control = (control & 0xFFFFFFBF) | ((value & 0x00000001) << 6); }
                get { return (control >> 6) & 0x00000001; }
            }

            public uint fTXContinueOnXoff
            {
                set { control = (control & 0xFFFFFF7F) | ((value & 0x00000001) << 7); }
                get { return (control >> 7) & 0x00000001; }
            }

            public uint fOutX
            {
                set { control = (control & 0xFFFFFEFF) | ((value & 0x00000001) << 8); }
                get { return (control >> 8) & 0x00000001; }
            }

            public uint fInX
            {
                set { control = (control & 0xFFFFFDFF) | ((value & 0x00000001) << 9); }
                get { return (control >> 9) & 0x00000001; }
            }

            public uint fErrorChar
            {
                set { control = (control & 0xFFFFFBFF) | ((value & 0x00000001) << 10); }
                get { return (control >> 10) & 0x00000001; }
            }

            public uint fNull
            {
                set { control = (control & 0xFFFFF7FF) | ((value & 0x00000001) << 11); }
                get { return (control >> 11) & 0x00000001; }
            }

            public uint fRtsControl
            {
                set { control = (control & 0xFFFFCFFF) | ((value & 0x00000003) << 12); }
                get { return (control >> 12) & 0x00000003; }
            }

            public uint fAbortOnError
            {
                set { control = (control & 0xFFFFBFFF) | ((value & 0x00000001) << 14); }
                get { return (control >> 14) & 0x00000001; }
            }

            public UInt16 wReserved;
            public UInt16 XonLim;
            public UInt16 XoffLim;
            public byte byteSize;
            public byte Parity;
            public byte StopBits;
            public sbyte XonChar;
            public sbyte XoffChar;
            public byte ErrorChar;
            public byte EofChar;
            public byte EvtChar;
            public UInt16 wReserved1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COMMTIMEOUTS
        {
            public UInt32 ReadIntervalTimeout; /* Maximum time between read chars. */
            public UInt32 ReadTotalTimeoutMultiplier; /* Multiplier of characters.        */
            public UInt32 ReadTotalTimeoutConstant; /* Constant in milliseconds.        */
            public UInt32 WriteTotalTimeoutMultiplier; /* Multiplier of characters.        */
            public UInt32 WriteTotalTimeoutConstant; /* Constant in milliseconds.        */
        }

        #endregion

        #region Private DLL

#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
        [DllImport("kernel32.dll")]
#endif
        private static extern bool GetDiskFreeSpaceEx(string directoryName, ref long freeBytesAvailable,
                                                       ref long totalBytes, ref long totalFreeBytes);

#if WINCE
        [DllImport("coredll", EntryPoint = "GetModuleFileNameW", SetLastError = true, CharSet=CharSet.Unicode,
            CallingConvention=CallingConvention.Winapi)]
#endif
#if WIN32
		[DllImport("kernel32", EntryPoint = "GetModuleFileNameW", SetLastError = true,CharSet=CharSet.Unicode, CallingConvention=CallingConvention.Winapi)]
#endif
        private static extern int _GetModuleFileName(
            IntPtr hModule,
            string lpFilename,
            uint nSize);
        #endregion

        #region Public DLL

#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
        [DllImport("kernel32.dll")]
#endif
        public static extern IntPtr CreateEvent(IntPtr lpEventAttributes, int bManualReset, int bInitialState,
                                                string lpName);

#if WINCE

        [DllImport("coredll.dll", EntryPoint = "EventModify", SetLastError = true)]
        private static extern int CEEventModify(IntPtr hEvent, uint function);
#endif
#if WIN32
        [DllImport("kernel32.dll", EntryPoint="SetEvent", SetLastError = true)]
		private static extern int WinSetEvent(IntPtr hEvent); 

		[DllImport("kernel32.dll", EntryPoint="ResetEvent", SetLastError = true)]
		private static extern int WinResetEvent(IntPtr hEvent); 

		[DllImport("kernel32.dll", EntryPoint="PulseEvent", SetLastError = true)]
		private static extern int WinPulseEvent(IntPtr hEvent);
#endif


#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
        [DllImport("kernel32.dll")]
#endif
        public static extern int EscapeCommFunction(IntPtr hFile, UInt32 dwFunc);

#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
        [DllImport("kernel32.dll")]
#endif
        public static extern bool PurgeComm(
            IntPtr handle,
            uint flags
            );

#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
        [DllImport("kernel32.dll")]
#endif
        public static extern int SetupComm(IntPtr hFile, Int32 dwInQueue, Int32 dwOutQueue);

#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
        [DllImport("kernel32.dll")]
#endif
        public static extern int SetCommState(IntPtr hFile, ref DCB dcb);

#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
        [DllImport("kernel32.dll")]
#endif
        public static extern int SetCommMask(IntPtr handle, CommEventFlags dwEvtMask);

#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
        [DllImport("kernel32.dll")]
#endif
        public static extern int SetCommTimeouts(IntPtr hFile, ref COMMTIMEOUTS timeouts);

#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
        [DllImport("kernel32.dll")]
#endif
        public static extern int WaitCommEvent(IntPtr hFile, ref CommEventFlags lpEvtMask, IntPtr lpOverlapped);

#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
        [DllImport("kernel32.dll")]
#endif
        public static extern int ClearCommError(IntPtr hFile, ref CommErrorFlags lpErrors, IntPtr lpStat);

#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
		[DllImport("kernel32.dll")]
#endif
        /// <summary>
        /// Эта функция выделяет определенное количество байтов из кучи. 
        /// В линейном Microsoft  Windows CE приложении , программируещее интерфейс среды (API),
        /// нет различия между локальной кучей и глобальной кучей.
        /// </summary>
        /// <param name="uFlags">[in] Определяет как выделять память.</param>
        /// <param name="uBytes">[in] Определяет количество байт для выделения из пямяти.</param>
        /// <returns>handle к новой распределенной памятиt. 
        /// NULL - неудачное выделение памяти.</returns>
        public static extern IntPtr LocalAlloc(uint uFlags, uint uBytes);

        /// <summary>
        /// Эта функция освобождает память и аннулирует handle.
        /// </summary>
        /// <param name="hMem">Handle к объекту памяти. 
        /// Этот handle возрощаеться при вызове LocalAlloc или LocalReAlloc</param>
        /// <returns>NULL - успешная работа. 
        /// handle на память - индикация невозможности освободить</returns>
#if WINCE
        [DllImport("coredll.dll")]
#endif
#if WIN32
		[DllImport("kernel32.dll")]
#endif
        public static extern IntPtr LocalFree(IntPtr hMem);

#if WINCE
        [DllImport("coredll", EntryPoint = "CreateFileMappingW", SetLastError = true, CharSet=CharSet.Unicode,
            CallingConvention=CallingConvention.Winapi)]
#endif
#if WIN32
		[DllImport("kernel32", EntryPoint = "CreateFileMappingW", SetLastError = true,CharSet=CharSet.Unicode, CallingConvention=CallingConvention.Winapi)]
#endif	
        public static extern IntPtr CreateFileMapping(
            IntPtr hFile,
            IntPtr lpFileMappingAttributes,
            uint flProtect,
            uint dwMaximumSizeHigh,
            uint dwMaximumSizeLow,
            string lpName
            );

#if WINCE
        [DllImport("coredll", EntryPoint = "CreateFileW", SetLastError = true, CharSet=CharSet.Unicode,
            CallingConvention=CallingConvention.Winapi)]
#endif
#if WIN32
        [DllImport("kernel32", EntryPoint = "CreateFileW", SetLastError = true, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
#endif
        public static extern IntPtr CreateFile(
            string fileName,
            uint desiredAccess,
            uint shareMode,
            IntPtr securityAttributes,
            uint creationDisposition,
            uint flagsAndAttributes,
            IntPtr templateFile
            );

#if WINCE
        [DllImport("coredll", EntryPoint = "CreateFileForMappingW", SetLastError = true, CharSet=CharSet.Unicode,
            CallingConvention=CallingConvention.Winapi)]
#endif
#if WIN32
		[DllImport("kernel32", EntryPoint = "CreateFileW", SetLastError = true,CharSet=CharSet.Unicode, CallingConvention=CallingConvention.Winapi)]
#endif
        public static extern IntPtr CreateFileForMapping(
            string fileName,
            uint desiredAccess,
            uint shareMode,
            IntPtr securityAttributes,
            uint creationDisposition,
            uint flagsAndAttributes,
            IntPtr templateFile
            );


#if WINCE
        [DllImport("coredll", EntryPoint = "MapViewOfFile", SetLastError = true,
            CallingConvention=CallingConvention.Winapi)]
#endif
#if WIN32
		[DllImport("kernel32", EntryPoint = "MapViewOfFile", SetLastError = true,CallingConvention=CallingConvention.Winapi)]
#endif
        public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh,
           uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern int GetLastError();

#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern int WriteFile(IntPtr hFile, byte[] lpBuffer, Int32 nNumberOfBytesToRead,
                                           out Int32 lpNumberOfBytesRead, IntPtr lpOverlapped);

#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern int ReadFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToRead,
                                          out int lpNumberOfBytesRead, IntPtr lpOverlapped);

#if WINCE
        [DllImport("coredll", EntryPoint = "FlushViewOfFile", SetLastError = true,
            CallingConvention=CallingConvention.Winapi)]
#endif
#if WIN32
        [DllImport("kernel32", EntryPoint = "FlushViewOfFile", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
#endif
        public static extern bool FlushViewOfFile(IntPtr lpBaseAddress, uint dwNumberOfBytesToFlush);

#if WINCE
        [DllImport("coredll", EntryPoint = "UnmapViewOfFile", SetLastError = true,
            CallingConvention=CallingConvention.Winapi)]
#endif
#if WIN32
		[DllImport("kernel32", EntryPoint = "UnmapViewOfFile", SetLastError = true,CallingConvention=CallingConvention.Winapi)]
#endif
        public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

#if WINCE
        [DllImport("coredll", EntryPoint = "CloseHandle", SetLastError = true,
            CallingConvention=CallingConvention.Winapi)]
#endif
#if WIN32
		[DllImport("kernel32", EntryPoint = "CloseHandle", SetLastError = true,CallingConvention=CallingConvention.Winapi)]
#endif
        public static extern bool CloseHandle(IntPtr handle);

#if WINCE
        [DllImport("coredll", SetLastError = true)]
#endif
#if WIN32
        [DllImport("kernel32", SetLastError = true)]
#endif
        public static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);

#if WINCE
        [DllImport("coredll", SetLastError = true)]
#endif
#if WIN32
        [DllImport("kernel32", SetLastError = true)]
#endif
        public static extern int CreateMutex(IntPtr handle, uint dwMilliseconds);

#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern bool ReleaseMutex(IntPtr handle);

#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern IntPtr GetForegroundWindow();

#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern bool SetForegroundWindow(IntPtr hWnd);

#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr lpdwProcessId);

#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern uint GetCurrentThreadId();

#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern bool IsIconic(IntPtr hWnd);




#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern IntPtr GetCapture(); 


#if WINCE
        [DllImport("coredll")]
#endif
#if WIN32
        [DllImport("kernel32")]
#endif
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow); 
        


#if WINCE
        [DllImport("coredll", EntryPoint = "WaitForSingleObject", SetLastError = true,
            CallingConvention=CallingConvention.Winapi)]
#endif
#if WIN32
        [DllImport("kernel32", EntryPoint = "WaitForSingleObject", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
#endif
        public static extern int WaitForSingleObject(
            IntPtr handle,
            uint dwMilliseconds
            );

        #endregion

        #region Public Metoth

        /// <summary>
        /// Возрощает информацию о свободном месте 
        /// </summary>
        /// <param name="directoryName">имя директории или диска</param>
        /// <param name="FreeBytesAvailable">всего свободных байт доступных для пользования</param>
        /// <param name="TotalBytes">всего байт на диске</param>
        /// <param name="TotalFreeBytes">всего свободных байт</param>
        public static void GetDiskFreeSpace(string directoryName, ref long FreeBytesAvailable, ref long TotalBytes,
                                            ref long TotalFreeBytes)
        {
            if (!GetDiskFreeSpaceEx(directoryName, ref FreeBytesAvailable, ref TotalBytes, ref TotalFreeBytes))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Error retrieving free disk space");
            }
        }

        public static void SetEvent(IntPtr hEvent)
        {
#if WINCE
            CEEventModify(hEvent, 3);
#endif
#if WIN32
            WinSetEvent(hEvent);
#endif
        }

        public static void ResetEvent(IntPtr hEvent)
        {
#if WINCE
            CEEventModify(hEvent, 2);
#endif
#if WIN32
            WinResetEvent(hEvent);
#endif
        }

        public static void PulseEvent(IntPtr hEvent)
        {
#if WINCE
            CEEventModify(hEvent, 1);
#endif
#if WIN32
            WinPulseEvent(hEvent);
#endif
        }

        public static string GetModuleFileName(IntPtr hModule)
        {
            string name = new string(' ', 256);
            int truelen = _GetModuleFileName(hModule, name, 256);
            return name.Substring(0, truelen);
        }

        #endregion


    }
}