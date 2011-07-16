using System;
using System.IO;

namespace WindowsLogger
{
    /// <summary>
    /// Запись лога в файл
    /// </summary>
    public class FileLogger : ILogger
    {
        string fullFilename;

        /// <summary>
        /// Взять экземпляр FileLogger
        /// </summary>
        /// <returns>ConsoleLogger</returns>
        public static ILogger Get(string fullfilename)
        {
            if (string.IsNullOrEmpty(fullfilename)) return null;
            FileLogger logger = new FileLogger();
            logger.fullFilename = fullfilename;
            return logger;
        }

        /// <summary>
        /// Взять экземпляр FileLogger ,только если other логгер пустой
        /// </summary>
        /// <param name="other">other логгер либо null</param>
        /// <returns>ILogger</returns>
        public static ILogger GetIfNull(ILogger other, string fullfilename)
        {
            if (other != null) return other;
            return Get(fullfilename);
        }

        public void Close()
        {
            WriteMessage((ushort)EventID.LogClose, "----- END LOG -----");
        }

        /// <summary>
        /// записать сообщение
        /// </summary> 
        public void WriteMessage(ushort id, object message)
        {
            try
            {
                File.AppendAllText(fullFilename, string.Format("{0}:[{1}] {2}", DateTime.Now,(int)id, message));
            }
            catch
            {
            }
        }

        public void WriteWarningMessage(ushort id, object message)
        {
            try
            {
                File.AppendAllText(fullFilename, string.Format("Warning: {0}:[{1}] {2}", DateTime.Now, (int)id, message));
            }
            catch
            {
            }
        }

        /// <summary>
        /// записать сообщение об ошибке
        /// </summary> 
        public void WriteErrorMessage(ushort id, object message)
        {
            try
            {
                File.AppendAllText(fullFilename, string.Format("ERROR: {0}:[{1}] {2}", DateTime.Now, (int)id, message));
            }
            catch
            {
            }
        }
    }
}
