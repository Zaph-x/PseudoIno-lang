using System;
using Parser;

/// <summary>
/// This method is raised when a token is invalid
/// </summary>
public class InvalidTokenException : Exception
{
    /// <summary>
    /// Raised when a token is unexpected and thus invalid
    /// </summary>
    /// <param name="message">The message to show the user</param>
            public InvalidTokenException(string message)
        {
            Console.Error.WriteLine(message);
        Parser.Parser.HasError = true;
        }
}