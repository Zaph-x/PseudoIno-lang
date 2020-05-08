using AbstractSyntaxTree.Objects;
using System;
namespace Contextual_analysis.Exceptions
{
    public class InvalidRangeException : Exception
    {
        public InvalidRangeException(string message)
        {
            Console.Error.WriteLine(message);
            // ASTHelper.HasError = true;
        }
    }
}