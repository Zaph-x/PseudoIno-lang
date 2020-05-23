using System;
using Parser;

public class InvalidTokenException : Exception
{
            public InvalidTokenException(string message)
        {
            Console.Error.WriteLine(message);
        Parser.Parser.HasError = true;
        }
}