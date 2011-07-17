
namespace Band.Storage
{
    public class ConnectException : System.InvalidOperationException
    {
        public ConnectException(string msg) : base(msg) { }
        public ConnectException(System.Exception inner) : base("connection is lost", inner) { }
    }
}
