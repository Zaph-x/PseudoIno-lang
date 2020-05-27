using System;
namespace CodeGeneration.Exceptions
{
    /// <summary>
    /// Raised when a piece of code is invalid
    /// </summary>
    public class InvalidCodeException : Exception
    {
        /// <summary>
        /// Raised when a combination is undersired in the code generator
        /// </summary>
        /// <param name="message">The message to show the user</param>
        /// <returns></returns>
        public InvalidCodeException(string message) : base(message)
        {
            Console.Error.WriteLine(message);
            CodeGenerationVisitor.HasError = true;
        }
    }
}