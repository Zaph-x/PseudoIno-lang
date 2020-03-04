using System;

namespace Lexer.Exceptions
{
    public class InvalidSyntaxException : Exception
    {
        public InvalidSyntaxException(string message) : base(message)
        {
            
        }

        public InvalidSyntaxException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}