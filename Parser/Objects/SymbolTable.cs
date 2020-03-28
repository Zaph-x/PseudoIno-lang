using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;
using System.Collections;
using System;

namespace Parser.Objects
{
    public class SymbolTable
    {
      public  int Level;
      public  string Name;
        bool Open;
        bool Close;

        public SymbolTable(int Level, string Name, bool Open, bool Close)
        {
            this.Level = Level;
            this.Name = Name;
            this.Open = Open;
            this.Close = Close;
        }
        public SymbolTable(int Level, string Name)
        {
            this.Level = Level;
            this.Name = Name;
           
        }
    }
}