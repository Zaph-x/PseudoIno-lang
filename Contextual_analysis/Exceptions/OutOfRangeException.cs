using AbstractSyntaxTree.Objects;
using System;
namespace Contextual_analysis.Exceptions
{
    /// <summary>
    /// An exception to raise when an array index is out of range
    /// </summary>
    public class OutOfRangeException : Exception
    {
        /// <summary>
        /// An exception to raise when an array is indexed with an index that is out of range.
        /// </summary>
        /// <param name="message">The message to show the user</param>
        public OutOfRangeException(string message) : base(message)
        {
            Console.Error.WriteLine(message);
            TypeChecker.HasError = true;
        }
    }
}