using AbstractSyntaxTree.Objects;
using System;
namespace Contextual_analysis.Exceptions
{
    public class NotDefinedException : Exception
    {
        public NotDefinedException(string message) : base(message)
        {
            Console.Error.WriteLine(message);
            TypeChecker.HasError = true;
        }
    }
}