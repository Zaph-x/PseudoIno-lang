using System;
using Parser;

[System.Serializable]
public class InvalidTokenException : Exception
{
            public InvalidTokenException(string message)
        {
            Console.Error.WriteLine(message);
            Parsenizer.HasError = true;
        }
}