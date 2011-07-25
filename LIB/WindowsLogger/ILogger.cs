
namespace Band.WindowsLogger
{
    public interface ILogger
    {
        void Close();


        /// <summary>
        /// записать сообщение
        /// </summary>        
        void WriteMessage(ushort id, object message);

        /// <summary>
        /// записать сообщение ошибки
        /// </summary>
        void WriteErrorMessage(ushort id, object message);


        void WriteWarningMessage(ushort id, object message);
   
    }
}
