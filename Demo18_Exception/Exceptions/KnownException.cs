using System;

namespace Demo18_Exception.Exceptions
{
    public class KnownException : IKnownException
    {
        private KnownException(string message, int errorCode)
        {
            ErrorCode = errorCode;
            Message   = message;
        }

        public string Message   { get; }
        public int    ErrorCode { get; }

        public static KnownException UnKnow() => new KnownException("未知錯誤", 99999);

        public static IKnownException FromKnownException(IKnownException exception)
        {
            return new KnownException(exception.Message, exception.ErrorCode);
        }
    }
}