using System;
using Parser;

public class DivisionByZeroException : Exception
{
    public DivisionByZeroException(string message)
    {
        Console.Error.WriteLine(message);
        Parsenizer.HasError = true;
    }
}