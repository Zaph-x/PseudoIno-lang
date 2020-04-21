using AbstractSyntaxTree.Objects;
using System;
namespace AbstractSyntaxTree.Exceptions
{
    public class UnexpectedSequenceException
    {
        public UnexpectedSequenceException(string message)
        {
            Console.Error.WriteLine(message);
            ASTHelper.HasError = true;
        }
    }
}