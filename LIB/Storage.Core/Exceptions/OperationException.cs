
namespace Band.Storage
{
    public class OperationException : System.InvalidOperationException
    {
        public OperationException(System.Exception inner) : base(null, inner) { }
    }
}
