using System;

namespace Lexer.Exceptions
{
    /// <summary>
    /// This exception will be thrown when the scanner or parser finds unexpexted syntax errors
    /// </summary>
    public class InvalidTypeException : Exception
    {
        /// <summary>
        /// The constructor of the exception, taking only one parameter.
        /// </summary>
        /// <param name="message">The message to show the user</param>
        public InvalidTypeException(string message) : base(message)
        {
        }
    }
}