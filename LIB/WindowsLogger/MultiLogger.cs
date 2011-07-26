
namespace Band.WindowsLogger
{
    /// <summary>
    /// объединение нескольких логгеров
    /// </summary>
    public class MultiLogger:ILogger
    {
        ILogger[] subloggers;
        int count = 0;

        /// <summary>
        /// Возвращает MultiLogger
        /// </summary>
        /// <param name="subloggers">масив логгеров для объединения</param>
        /// <returns>MultiLogger или null(если масив пуст или все элименты равны null)</returns>
        public static ILogger Get(ILogger[] subloggers)
        {
            if (subloggers == null) return null;
            if (subloggers.Length == 0) return null;
            MultiLogger log = new MultiLogger();
            log.subloggers = new ILogger[subloggers.Length];
            for (int i = 0; i < subloggers.Length; i++)
            {
                if (subloggers[i] == null) continue;
                log.subloggers[log.count] = subloggers[i];
                log.count++;
            }
            if (log.count < 1) return null;
            return log;
        }
        /// <summary>
        /// Возвращает экземпляр MultiLogger ,только если other логгер пустой
        /// </summary>
        /// <param name="other">other логгер</param>
        /// <param name="subloggers">масив логгеров для объединения</param>
        /// <returns>MultiLogger или null(если масив пуст или все элименты равны null и при всем этом other тоже равен null)</returns>
        public static ILogger GetIfNull(ILogger other, ILogger[] subloggers)
        {
            if (other != null) return other;
            return Get(subloggers);
        }

        public void Close()
        {
            for (int i = 0; i < count; i++) subloggers[i].Close();
        }

        /// <summary>
        /// записать сообщение
        /// </summary> 
        public void WriteMessage(ushort id, object message)
        {
            for (int i = 0; i < count; i++) subloggers[i].WriteMessage(id,message);
        }
        public void WriteWarningMessage(ushort id, object message)
        {
            for (int i = 0; i < count; i++) subloggers[i].WriteWarningMessage(id,message);
        }
        /// <summary>
        /// записать сообщение об ошибке
        /// </summary> 
        public void WriteErrorMessage(ushort id, object message)
        {
            for (int i = 0; i < count; i++) subloggers[i].WriteErrorMessage(id,message);
        }
    }
}
