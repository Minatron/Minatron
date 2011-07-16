namespace ImagesStoreSystem.DBProvider.Core
{
    public class OperationException : System.InvalidOperationException
    {
        public OperationException(System.Exception inner) : base(null, inner) { }
    }
}
