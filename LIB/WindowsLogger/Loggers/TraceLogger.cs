using System;
using System.Diagnostics;

namespace Band.WindowsLogger
{
    /// <summary>
    /// Запись лога в Trace .NET
    /// </summary>
    public class TraceLogger: ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ILogger Get()
        {
            #if TRACE
            return new TraceLogger();
            #else
            return null;
            #endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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
            Trace.WriteLine(string.Format("{0}:[{1}] {2}", DateTime.Now,(int)id, message));
        }

        /// <summary>
        /// записать сообщение
        /// </summary> 
        public void WriteWarningMessage(ushort id, object message)
        {
            Trace.WriteLine(string.Format("{0}:[{1}] {2}", DateTime.Now, (int)id, message), "Warning");
        }

        /// <summary>
        /// записать сообщение об ошибке
        /// </summary> 
        public void WriteErrorMessage(ushort id, object message)
        {
            Trace.WriteLine(string.Format("{0}:[{1}] {2}", DateTime.Now, (int)id, message), "ERROR");
        }   
    }
}
