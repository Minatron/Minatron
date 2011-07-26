using System.Diagnostics;

namespace Band.WindowsLogger
{
    /// <summary>
    /// Запись лога в Event Log Windows
    /// </summary>
    public class EventLogger: ILogger
    {
        private EventLog log;

        static void SetOptionsIfNeed(EventLog log)
        {
            if (log.OverflowAction != OverflowAction.OverwriteAsNeeded)
            {
                log.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 1);
                log.MaximumKilobytes = 512000;                
            }
        }

        /// <summary>
        /// Возвращает экземпляр EventLogger
        /// </summary>
        /// <param name="logname">имя лог файла</param>
        /// <param name="sourcename">имя источника</param>
        /// <param name="autoRegister">автоматически регистрировать лог файл при необходимости (лог будет доступен при следующем запуске приложения)</param>
        /// <returns>EventLogger или null(если лог файл не зарегистрирован) </returns>
        public static ILogger Get(string logname, string sourcename, bool autoRegister)
        {
            if (!EventLog.Exists(logname))
            {
                if (autoRegister) EventLog.CreateEventSource(sourcename, logname);
                return null;
            }

            if (!EventLog.SourceExists(sourcename))
            {
                EventLog.CreateEventSource(sourcename, logname);
            }
            EventLogger logger = new EventLogger();
            logger.log = new EventLog();
            logger.log.Source = sourcename;
            SetOptionsIfNeed(logger.log);            
            return logger;
        }

        /// <summary>
        /// Возвращает экземпляр EventLogger ,только если other логгер пустой
        /// </summary>
        /// <param name="other">other логгер</param>
        /// <param name="logname">имя лог файла</param>
        /// <param name="sourcename">имя источника</param>
        /// <returns>EventLogger или null(если лог файл не зарегистрирован и other тоже равен null) </returns>
        public static ILogger GetIfNull(ILogger other, string logname, string sourcename)
        {
            if (other != null) return other;
            return Get(logname, sourcename, false);
        }

        /// <summary>
        /// Регистрация лога
        /// </summary>
        /// <param name="logname">имя лога</param>
        public static bool Register(string logname, string sourcename)
        {
            if (!EventLog.Exists(logname))
            {
                EventLog.CreateEventSource(sourcename, logname);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Отрегистрировать лог
        /// </summary>
        /// <param name="logname">имя лога</param>
        public static bool Unregister(string logname)
        {
            if (!EventLog.Exists(logname)) return false;
            EventLog.Delete(logname);
            return true;
        }    

        public void Close()
        {
            if (log != null)
            {
                log.Close();
                log = null;
            }
        }

        /// <summary>
        /// записать сообщение
        /// </summary> 
        public void WriteMessage(ushort id, object message)
        {
            log.WriteEntry(message.ToString(),EventLogEntryType.Information,(int)id);
        }

        /// <summary>
        /// записать сообщение
        /// </summary> 
        public void WriteWarningMessage(ushort id, object message)
        {
            log.WriteEntry(message.ToString(), EventLogEntryType.Warning, (int)id);
        }

        /// <summary>
        /// записать сообщение об ошибке
        /// </summary> 
        public void WriteErrorMessage(ushort id, object message)
        {
            log.WriteEntry(message.ToString(), EventLogEntryType.Error, (int)id);
        }       
    }
}
