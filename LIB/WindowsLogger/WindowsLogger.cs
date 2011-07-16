using System.Diagnostics;

namespace WindowsLogger
{
    /// <summary>
    /// !!! ЭТОТ ИНТЕРФЕЙС ОСТАВЛЕН ДЛЯ СОВМЕСТИМОСТИ !!!
    /// </summary>
    public class WindowsLogger
    {
        string logName;
        string sourceName;
        ILogger logger;

        /// <summary>        
        /// Подключение к логу в Event Log Windows, а если лог не зарегистрирован то -  лог будет напрвлен в консоль приложения        
        /// !!! ЭТОТ ИНТЕРФЕЙС ОСТАВЛЕН ДЛЯ СОВМЕСТИМОСТИ !!!
        /// НОВЫЙ ИНТЕРФЕЙС -> ILogger [ILogger] = ConsoleLogger.GetIfNull(EventLogger.Get(logName,sourceName,false)); 
        /// </summary>        
        public WindowsLogger(string logName, string sourceName)
        {            
            this.logName = logName;
            this.sourceName = sourceName;
            logger = ConsoleLogger.GetIfNull(EventLogger.Get(logName,sourceName,false));            
        }
       
        /// <summary>
        /// !!! ЭТОТ ИНТЕРФЕЙС ОСТАВЛЕН ДЛЯ СОВМЕСТИМОСТИ !!!
        /// НОВЫЙ ИНТЕРФЕЙС -> EventLogger.Register(logName, sourceName); 
        /// </summary>
        public void InstallLog()
        {            
            UninstallLog();
            EventLogger.Register(logName, sourceName);            
        }

        /// <summary>
        /// !!! ЭТОТ ИНТЕРФЕЙС ОСТАВЛЕН ДЛЯ СОВМЕСТИМОСТИ !!!
        /// НОВЫЙ ИНТЕРФЕЙС -> EventLogger.Unregister(logName);  
        /// </summary>
        public void UninstallLog()
        {            
            if (logger is EventLogger) logger.Close();              
            EventLogger.Unregister(logName);            
        }

       
        /// <summary>
        /// !!! ЭТОТ ИНТЕРФЕЙС ОСТАВЛЕН ДЛЯ СОВМЕСТИМОСТИ !!!
        /// НОВЫЙ ИНТЕРФЕЙС -> [ILogger].WriteMessage(object);
        /// </summary>
        /// <param name="message"></param>
        public void WriteMessage(string message)
        {
            logger.WriteMessage((ushort)EventID.None, message);
        }

        /// <summary>
        /// !!! ЭТОТ ИНТЕРФЕЙС ОСТАВЛЕН ДЛЯ СОВМЕСТИМОСТИ !!!
        /// НОВЫЙ ИНТЕРФЕЙС -> [ILogger].WriteErrorMessage(object);
        /// </summary>
        /// <param name="message"></param>
        public void WriteErrorMessage(string message)
        {
            logger.WriteErrorMessage((ushort)EventID.None, message);            
        }              
    }
}
