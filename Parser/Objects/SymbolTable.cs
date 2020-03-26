using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;
using System.Collections;

namespace Parser.Objects
{
    public class SymbolTable
    {
        Dictionary<string,TokenType> symbolTable = new Dictionary<string, TokenType>();

        //use stack to store scopes in dictionary. The last value is the Outermost scope asf
        Stack<Dictionary<string, TokenType>> StackScope = new Stack<Dictionary<string, TokenType>>();


        // Not used yet but have to at some point
        private List<Dictionary<string,TokenType>> scopeLayers = new List<Dictionary<string, TokenType>>();
        
        public bool EnterSymbol(string name, TokenType type)
        {
            symbolTable.Add(name, type);
            {
                throw new InvalidSyntaxException($"Could not add {type.ToString()} {name} symbol to table");
            }
        }

        public Token RetrieveSymbol(string name, TokenType type)
        {
            //if (DicSymbolTable.TryGetValue(name, out TokenType tokenType))
            return new ScannerToken(type, name, 1, 1);
            //throw new InvalidSyntaxException($"Symbol {name} was not in symbol table");
        }
        public void AddSym(TokenType symbol)
        {

        }
        public void RemoveSym(TokenType symbol)
        {

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