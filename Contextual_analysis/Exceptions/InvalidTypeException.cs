using AbstractSyntaxTree.Objects;
using System;
namespace Contextual_analysis.Exceptions
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string message)
        {
            Console.Error.WriteLine(message);
            // ASTHelper.HasError = true;
        }
    }
}