using System;
namespace Band.WindowsLogger
{
    /// <summary>
    /// Запись лога а консоль приложения
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Взять экземпляр ConsoleLogger
        /// </summary>
        /// <returns>ConsoleLogger</returns>
        public static ILogger Get()
        {
            return new ConsoleLogger();
        }

        /// <summary>
        /// Взять экземпляр ConsoleLogger ,только если other логгер пустой
        /// </summary>
        /// <param name="other">other логгер либо null</param>
        /// <returns>ILogger</returns>
        public static ILogger GetIfNull(ILogger other)
        {
            if (other != null) return other;
            return Get();
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
            Console.WriteLine(string.Format("{0}:[{1}] {2}",DateTime.Now,(int)id,message));
        }
        public void WriteWarningMessage(ushort id, object message)
        {
            Console.WriteLine(string.Format("Warning: {0}:[{1}] {2}", DateTime.Now, (int)id, message));
        }
        /// <summary>
        /// записать сообщение об ошибке
        /// </summary> 
        public void WriteErrorMessage(ushort id, object message)
        {
            Console.WriteLine(string.Format("ERROR: {0}:[{1}] {2}", DateTime.Now, (int)id, message));
        }
    }
}
