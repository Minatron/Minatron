using Band.WindowsLogger;

namespace Band.WeightData.Terminal
{
    public class Logger : ILogger
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
        }

        ILogger _log;

        public Logger(string name)
        {
            _log = MultiLogger.Get(
                new ILogger[]
                 {
                 ConsoleLogger.Get(),                                                                                          
                 EventLogger.Get("WeightDataTerminalLog","WeightDataTerminal_"+name,true)
                 }
            );
        }

        public void Close()
        {
            _log.Close();
        }

        public void WriteMessage(EventID id)
        {
            WriteMessage((ushort)id, "");
        }
        public void WriteMessage(EventID id, object message)
        {
            WriteMessage((ushort)id, message);
        }

        public void WriteErrorMessage(ushort id, object message)
        {
            WriteMessage(id, message);
        }
        public void WriteWarningMessage(ushort id, object message)
        {
            WriteMessage(id, message);
        }
        public void WriteMessage(ushort id, object message)
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
                default:
                    _log.WriteMessage(id, message);
                    break;
            }

            _log.WriteMessage(id, message);
        }
    }
}
