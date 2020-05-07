using System;

namespace SymbolTable.Exceptions
{
    public class SymbolNotFoundException : Exception
    {
        public SymbolNotFoundException(string message) : base(message) {}
    }
}