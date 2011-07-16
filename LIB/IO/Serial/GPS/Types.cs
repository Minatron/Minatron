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
    /// Направление координат
    /// </summary>
    public enum CardinalDirection
    {
        /// <summary>
        /// Север
        /// </summary>
        North = 0,
        /// <summary>
        /// Восток
        /// </summary>
        East = 1,
        /// <summary>
        /// Юг
        /// </summary>
        South = 2,
        /// <summary>
        /// Запад
        /// </summary>
        West = 4,
        /// <summary>
        /// СевероЗапад
        /// </summary>
        NorthWest = 5,
        /// <summary>
        /// СевероВосток
        /// </summary>
        NorthEast = 6,
        /// <summary>
        /// ЮгоЗапад
        /// </summary>
        SouthWest = 7,
        /// <summary>
        /// ЮгоВосток
        /// </summary>
        SouthEast = 8,
        /// <summary>
        /// Неподвижный
        /// </summary>
        Stationary = 9
    }

    public enum GPMessage : uint
    {
        /// <summary>
        /// GPS данные о местоположении
        /// </summary>
        GPGGA = 1 ,
        /// <summary>
        /// GPS географическое положение - Широта/Долгота
        /// </summary>
        GPGLL = 2,
        /// <summary>
        /// GPS факторы точности и активные спутники
        /// </summary>
        GPGSA = 4 ,
        /// <summary>
        /// GPS Видимые спутники
        /// </summary>
        GPGSV = 8,
        /// <summary>
        /// GPS минимум навигационных данных
        /// </summary>
        GPRMC = 16,
        /// <summary>
        /// GPS истинное направление курса и скорость относительно земли
        /// </summary>
        GPVTG = 32,
        /// <summary>
        /// GPS время и дата
        /// </summary>
        GPZDA = 64



    }
    public delegate void GPSSerialDataReceivedDelegate(object sender,Lacross.IO.SerialPort.SerialData e,string text);
    public delegate void GPSDataReceivedDelegate(object sender, int indexrow);
    public delegate void GPSAllDataParsedDelegate(object sender);
}
