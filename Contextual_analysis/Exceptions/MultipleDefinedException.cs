using AbstractSyntaxTree.Objects;
using System;
namespace Contextual_analysis.Exceptions
{
    /// <summary>
    /// An exception raised when two functions with same amount of parameters and name has been defined
    /// </summary>
    public class MultipleDefinedException : Exception
    {
        /// <summary>
        /// An exception raised when two functions with the same amount of parameters and the same name are defined. This is handled in the typechecker
        /// </summary>
        /// <param name="message">The message to show the user</param>
        public MultipleDefinedException(string message) : base(message)
        {
            Console.Error.WriteLine(message);
            TypeChecker.HasError = true;
        }
    }
}