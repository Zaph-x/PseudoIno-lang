using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;
using System.Collections;
using System;

namespace Parser.Objects
{
    public class SymbolTable
    {
        //Dictionary<string,TokenType> symbolTable = new Dictionary<string, TokenType>();

        //use stack/list to store scopes in the symboltable. The first value is the Outermost scope and so on.
        //public Stack <Dictionary<string, TokenType>> StackScope = new Stack <Dictionary<string, TokenType>>();
        public List<Dictionary<string, TokenType>> ScopeList = new List<Dictionary<string, TokenType>>();
        public int Currentscope = 0;
        public void EnterSymbol(string name, TokenType type)
        {
            ScopeList.Add(new Dictionary<string, TokenType>());
            try
            {
                ScopeList[ScopeList.Count].Add(name, type);
                Currentscope = ScopeList.Count;
            }
            catch (Exception ex)
            {
                throw ex;
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