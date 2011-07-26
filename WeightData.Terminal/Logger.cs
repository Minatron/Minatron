using Band.WindowsLogger;
using System;
using System.Text;


namespace Band.WeightData.Terminal
{
    public class Logger 
    {
        public enum EventID : ushort
        {
            None = 0,
            LogBegin = 1,
            LogClose = 2,
            ServiceStart = 3,
            ServiceStop = 4,
            ServiceCrash = 13001,
            ServiceCannotStart = 13002,

            StoreData = 16000,
        }

        static ILogger _log = null;

        public static void InitWithName(string name)
        {
            _log = MultiLogger.Get(
                new ILogger[]
                 {
                 ConsoleLogger.Get(),
                 EventLogger.Get("WeightDataTerminalLog","WeightDataTerminal_"+name,true)
                 });
            WriteMessage(Logger.EventID.ServiceStart);
        }

        public static void Close()
        {
            WriteMessage(Logger.EventID.ServiceStop);
            _log.Close();
        }

        public static void WriteMessage(EventID id)
        {
            WriteMessage((ushort)id, "");
        }
        public static void WriteMessage(EventID id, object message)
        {
            WriteMessage((ushort)id, message);
        }
        
        public static void WriteMessage(ushort id, object message)
        {
            if (message == null) message = "";

            switch ((EventID)id)
            {
                case EventID.ServiceStart:
                    _log.WriteMessage(id, @"Начало работы");
                    break;
                case EventID.ServiceStop:
                    _log.WriteMessage(id, @"Завершение работы");
                    break;
                case EventID.ServiceCannotStart:
                    _log.WriteErrorMessage(id, message);
                    break;
                case EventID.ServiceCrash:
                    _log.WriteErrorMessage(id, message);
                    break;
                case EventID.StoreData:
                    _log.WriteMessage(id,string.Format("Получен пакет {0}", message));
                    break;
                default:
                    _log.WriteMessage(id, message);
                    break;
            }
        }
    }
}
