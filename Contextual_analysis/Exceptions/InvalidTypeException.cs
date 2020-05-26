using AbstractSyntaxTree.Objects;
using System;
namespace Contextual_analysis.Exceptions
{
    /// <summary>
    /// Raised when a type does not match its counterpart
    /// </summary>
    public class InvalidTypeException : Exception
    {
        /// <summary>
        /// Raised in the typechecker when two types to not match
        /// </summary>
        /// <param name="message">The message to show the user</param>
        public InvalidTypeException(string message) : base(message)
        {
            Console.Error.WriteLine(message);
            TypeChecker.HasError = true;
        }
    }
}