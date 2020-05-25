using System;
using Parser;

/// <summary>
/// Raised when a number is divided by zero.
/// </summary>
public class DivisionByZeroException : Exception
{
    /// <summary>
    /// Raised if a number is divided by zero. This does not take variables into account.
    /// </summary>
    /// <param name="message">The message to show the user</param>
    public DivisionByZeroException(string message)
    {
        Console.Error.WriteLine(message);
        Parser.Parser.HasError = true;
    }
}