using System;
namespace Lacross.IO.SerialPort
{
    public enum Parity : byte
    {
        None =0,
        Odd =1,
        Even =2,
        Mark =3,        
        Space =4
    }
    public enum SerialData
    {
        Chars,
        Eof
    }
    public enum SerialError
    {
        Frame,
        IOE,
        Mode,
        Overrun,
        RXOver,
        RXParity,
        TXFull
    }
    public enum SerialPinChange
    {
        Break,
        CDChanged,
        CtsChanged,
        DsrChanged,
        Ring
    }
    public enum StopBits
    {
        One = 0,
        OnePointFive =1,
        Two =2
    }

    public delegate void SerialDataReceivedDelegate     (object sender, SerialData e);
    public delegate void SerialErrorReceivedDelegate    (object sender, SerialError e);
    public delegate void SerialPinChangedDelegate       (object sender, SerialPinChange e);

    public class SerialPortException : Exception
    {
        public SerialPortException(string desc) : base(desc) { }
    }
}