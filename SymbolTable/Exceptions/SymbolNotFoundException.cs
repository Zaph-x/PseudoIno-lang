using System;

namespace SymbolTable.Exceptions
{   
    /// <summary>
    /// An exception raised if a symbol is not found in the symbol table
    /// </summary>
    public class SymbolNotFoundException : Exception
    {
        /// <summary>
        /// The constructor of the exception to raise if a symbol is not found
        /// </summary>
        /// <param name="message">The message to show the user</param>
        /// <returns></returns>
        public SymbolNotFoundException(string message) : base(message) {}
    }
}