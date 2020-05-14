using AbstractSyntaxTree.Objects;
using System;
namespace Contextual_analysis.Exceptions
{
    public class InvalidRangeException : Exception
    {
        public InvalidRangeException(string message) : base(message)
        {
            Console.Error.WriteLine(message);
            TypeChecker.HasError = true;
        }
    }
}