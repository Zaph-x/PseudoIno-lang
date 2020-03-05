using System;

namespace Core.Exceptions
{
    public class EncodingNotSupportedException : Exception
    {
        public EncodingNotSupportedException(string message) : base(message)
        { }
        public EncodingNotSupportedException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}