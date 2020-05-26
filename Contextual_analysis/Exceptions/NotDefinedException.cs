using AbstractSyntaxTree.Objects;
using System;
namespace Contextual_analysis.Exceptions
{
    /// <summary>
    /// An exception to raise when something is used but not defined
    /// </summary>
    public class NotDefinedException : Exception
    {
        /// <summary>
        /// An exception to raise when something is not defined but still used. This is handled in the typechecker
        /// </summary>
        /// <param name="message">The message to show the user</param>
        public NotDefinedException(string message) : base(message)
        {
            Console.Error.WriteLine(message);
            TypeChecker.HasError = true;
        }
    }
}