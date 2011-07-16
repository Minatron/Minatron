using System;

namespace Lacross.IO.GPS
{
    internal enum NMEAChars : byte
    {
        Dollar = 0x24,
        Star = 0x2A,
        Comma = 0x2C,
        Dot = 0x2E

    }
    /// <summary>
    /// ����������� ���������
    /// </summary>
    public enum CardinalDirection
    {
        /// <summary>
        /// �����
        /// </summary>
        North = 0,
        /// <summary>
        /// ������
        /// </summary>
        East = 1,
        /// <summary>
        /// ��
        /// </summary>
        South = 2,
        /// <summary>
        /// �����
        /// </summary>
        West = 4,
        /// <summary>
        /// �����������
        /// </summary>
        NorthWest = 5,
        /// <summary>
        /// ������������
        /// </summary>
        NorthEast = 6,
        /// <summary>
        /// ��������
        /// </summary>
        SouthWest = 7,
        /// <summary>
        /// ���������
        /// </summary>
        SouthEast = 8,
        /// <summary>
        /// �����������
        /// </summary>
        Stationary = 9
    }

    public enum GPMessage : uint
    {
        /// <summary>
        /// GPS ������ � ��������������
        /// </summary>
        GPGGA = 1 ,
        /// <summary>
        /// GPS �������������� ��������� - ������/�������
        /// </summary>
        GPGLL = 2,
        /// <summary>
        /// GPS ������� �������� � �������� ��������
        /// </summary>
        GPGSA = 4 ,
        /// <summary>
        /// GPS ������� ��������
        /// </summary>
        GPGSV = 8,
        /// <summary>
        /// GPS ������� ������������� ������
        /// </summary>
        GPRMC = 16,
        /// <summary>
        /// GPS �������� ����������� ����� � �������� ������������ �����
        /// </summary>
        GPVTG = 32,
        /// <summary>
        /// GPS ����� � ����
        /// </summary>
        GPZDA = 64



    }
    public delegate void GPSSerialDataReceivedDelegate(object sender,Lacross.IO.SerialPort.SerialData e,string text);
    public delegate void GPSDataReceivedDelegate(object sender, int indexrow);
    public delegate void GPSAllDataParsedDelegate(object sender);
}
