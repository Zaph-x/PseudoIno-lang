using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;
using System.Collections;
using System;

namespace Parser.Objects
{
    public class SymbolTable
    {

        Dictionary<string, TokenType> DicSymbolTable = new Dictionary<string, TokenType>();
      

        //public SymbolTable()
        //{
           
        //}
        public TokenType RetrieveSymbol(string name)
        {
            if (DicSymbolTable.TryGetValue(name, out TokenType tokenType))
            {
                return DicSymbolTable[name];
            }
            throw new InvalidSyntaxException($"Symbol {name} was not in symbol table");
        }
        public void AddSym(string name, TokenType type)
        {

            if (DicSymbolTable.TryAdd(name, type)) 
            {
                DicSymbolTable.Add(name, type);
            }
            throw new InvalidSyntaxException($"Symbol {name} was not added in symbol table");
        }
        public void RemoveSym(string name, TokenType type)
        {
            DicSymbolTable.Remove(name);
        }

        public void OpenScope()
        {

        }
        public void CloseScope()
        {

        }
        public bool DeclaredLocally(TokenType symName)
        {
            return true;
        }
    }
}