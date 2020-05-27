using AbstractSyntaxTree.Objects;
using System;

namespace Contextual_analysis.Exceptions
{
    /// <summary>
    /// An exception thrown when a range is invalid
    /// </summary>
    public class InvalidReturnException : Exception
    {
        /// <summary>
        /// An exception thrown when a range is invalid
        /// </summary>
        /// <param name="message">The message to show the user</param>
        public InvalidReturnException(string message) : base(message)
        {
            Console.Error.WriteLine(message);
            TypeChecker.HasError = true;
        }
    }
}