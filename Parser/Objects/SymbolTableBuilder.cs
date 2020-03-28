using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;
using System.Collections;
using System;

namespace Parser.Objects
{
    class SymbolTableBuilder
    {
        int Current;
        //list for lookup of scope and name to see if there are duplicates
        List<SymbolTable> ScopeTracker = new List<SymbolTable>();
        //List for symboltable content name and type
        List<Dictionary<string, TokenType>> Symboltable = new List<Dictionary<string, TokenType>>(); 
        void OpenScope()
        {
         
        }
        void CloseScope()
        {
         
        }
        public TokenType RetrieveSymbol(string name)
        {
            //if (DicSymbolTable.TryGetValue(name, out TokenType tokenType))
            //{
            //    return DicSymbolTable[name];
            //}
            throw new InvalidSyntaxException($"Symbol {name} was not in symbol table");
        }
        /// <summary>
        /// Add symbol to dictionary Symboltable and list of scope Scopetracker
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public void AddSym(string name, TokenType type)
        {
           if(LookUp(name,Current)==true)
            {
                Symboltable.Add(new Dictionary<string, TokenType> ;
            }
            //if (DicSymbolTable.TryAdd(name, type))
            //{
            //    DicSymbolTable.Add(name, type);
            //}
            throw new InvalidSyntaxException($"Symbol {name} was not added in symbol table, since it is there already");
        }
        public bool LookUp(string Name, int Level)
        {
      
            if (ScopeTracker.Contains(new SymbolTable(Level, Name)))
            {
                throw new InvalidSyntaxException($"Symbol { Name } was not added in the symbol table, since it exist allready there at scope {Level}");
            }
            return true;
        }
    }
}
