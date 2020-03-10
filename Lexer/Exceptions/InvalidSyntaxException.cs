using System;

namespace Lexer.Exceptions
{
    /// <summary>
    /// This exception will be thrown when the scanner or parser finds unexpexted syntax errors
    /// </summary>
    public class InvalidSyntaxException : Exception
    {
        /// <summary>
        /// The constructor of the exception, taking only one parameter.
        /// </summary>
        /// <param name="message">The message to show the user</param>
        public InvalidSyntaxException(string message) : base(message)
        {
            
        }

        /// <summary>
        /// The constructor of the exception, taking two parameters.
        /// </summary>
        /// <param name="message">The message to show the user</param>
        /// <param name="innerException">The inner exception of the exception</param>
        public InvalidSyntaxException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}