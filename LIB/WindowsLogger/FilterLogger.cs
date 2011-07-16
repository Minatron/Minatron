using System;

namespace WindowsLogger
{
    /// <summary>
    /// Фильтрует запись лога по типу сообщения
    /// </summary>
    public class FilterLogger : ILogger
    {
        public const int FILTER_ERROR = 0;
        public const int FILTER_ERROR_AND_WARNING = 1;
        public const int FILTER_ALL = 2;

        ILogger logger;
        int filter = 0;

         
        public static ILogger Get(int filter,ILogger logger)
        {
            if (logger == null) return null;
            FilterLogger log = new FilterLogger();
            log.logger = logger;
            log.filter = filter;            
            return log;
        }
        
        public static ILogger GetIfNull(ILogger other, int filter,ILogger logger)
        {
            if (other != null) return other;
            return Get(filter,logger);
        }

        public void Close()
        {
            logger.Close();
        }

        /// <summary>
        /// записать сообщение
        /// </summary> 
        public void WriteMessage(ushort id, object message)
        {
            if (filter>1) logger.WriteMessage(id,message);
        }
        public void WriteWarningMessage(ushort id, object message)
        {
            if (filter>0) logger.WriteWarningMessage(id, message);
        }
        /// <summary>
        /// записать сообщение об ошибке
        /// </summary> 
        public void WriteErrorMessage(ushort id, object message)
        {
            logger.WriteErrorMessage(id,message);
        }

    }

}

