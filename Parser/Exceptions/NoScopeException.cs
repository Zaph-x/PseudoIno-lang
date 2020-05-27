using System;
using Parser;
/// <summary>
/// This exception is raised when a scope is not defined
/// </summary>
public class NoScopeException : Exception
{
    /// <summary>
    /// This exception is raised when a scope is not found or defined
    /// </summary>
    /// <param name="message">The message to show the user</param>
    public NoScopeException(string message)
    {
        Console.Error.WriteLine(message);
        Parser.Parser.HasError = true;
    }
}