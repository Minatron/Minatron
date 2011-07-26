
namespace Band.WeightData.Terminal
{
    public class DataBase
    {
        bool _isWork = false;

        public DataBase()
        {
            _isWork = true;
        }

        public bool IsConnect
        {
            get { return _isWork; }
        }

        public void Store(string packet)
        {
            Logger.WriteMessage(Logger.EventID.StoreData, packet);
        }

        public void Close()
        {
            if (_isWork)
            {
                _isWork = false;
            }
        }
    }
}
