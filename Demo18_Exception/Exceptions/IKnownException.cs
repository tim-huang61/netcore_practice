namespace Demo18_Exception.Exceptions
{
    public interface IKnownException
    {
        string Message   { get; }
        int    ErrorCode { get; }
    }
}