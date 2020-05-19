using System;
namespace CodeGeneration.Exceptions
{
    public class InvalidCodeException : Exception
    {
        public InvalidCodeException(string message) : base(message)
        {
            Console.Error.WriteLine(message);
            CodeGenerationVisitor.HasError = true;
        }
    }
}