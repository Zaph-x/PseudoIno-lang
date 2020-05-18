using AbstractSyntaxTree.Objects;
using System;
namespace Contextual_analysis.Exceptions
{
    public class InvalidReturnException : Exception
    {
        public InvalidReturnException(string message) : base(message)
        {
            Console.Error.WriteLine(message);
            TypeChecker.HasError = true;
        }
    }
}