using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;

namespace Parser.Objects
{
    public class SymbolTable
    {
        private Dictionary<string,TokenType> symbolTable = new Dictionary<string, TokenType>();
        
        // Not used yet but have to at some point
        private List<Dictionary<string,TokenType>> scopeLayers = new List<Dictionary<string, TokenType>>();
        
        public bool EnterSymbol(string name, TokenType type)
        {
            if (symbolTable.TryAdd(name,type))
            {
                return true;
            }
            throw new InvalidSyntaxException($"Could not add {type.ToString()} {name} symbol to table");
        }

        public Token RetrieveSymbol(string name)
        {
            if (symbolTable.TryGetValue(name, out TokenType tokenType))
                return new Token(tokenType,name,1,1);
            throw new InvalidSyntaxException($"Symbol {name} was not in symbol table");
        }
    }
}