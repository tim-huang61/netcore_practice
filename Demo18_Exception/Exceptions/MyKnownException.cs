using System;

namespace Demo18_Exception.Exceptions
{
    public class MyKnownException : Exception, IKnownException
    {
        public MyKnownException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }
    }
}