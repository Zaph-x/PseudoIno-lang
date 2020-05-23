using System;
using Parser;

public class NoScopeException : Exception
{
            public NoScopeException(string message)
        {
            Console.Error.WriteLine(message);
        Parser.Parser.HasError = true;
        }
}