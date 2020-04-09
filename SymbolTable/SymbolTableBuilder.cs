using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;
using System.Collections;
using System;
using System.Linq;

namespace SymbolTable
{ 
    class SymbolTableBuilder
    {
    //    //current level scope
    //    int currenLevel=0;
    //    public int CurrentLevelProp
    //    {
    //        get { return currenLevel; }
    //        set { currenLevel = value; }
    //    }

    //    //test
    //    //Dictionary for lookup of scope and Name to see if there are duplicates. The dictionary also have open and close params. Tuple<level,depth,open, close>
    //    Dictionary<string,Tuple<int,int,bool,bool>> ScopeTracker = new Dictionary<string, Tuple<int,int,bool, bool>>();
    //    //List for symboltable content Name and Type
    //    List<Dictionary<string, TokenType>> Symboltable = new List<Dictionary<string, TokenType>>(); 
    //    void OpenScope()
    //    {
         
    //    }
    //    void CloseScope(string Name)
    //    {
    //        ScopeTracker[Name] = Tuple.Create(ScopeTracker[Name].Item1, ScopeTracker[Name].Item2, ScopeTracker[Name].Item3, ScopeTracker[Name].Item4==true);
            
    //    }
    //    public TokenType RetrieveSymbol(string Name)
    //    {
    //        //if (DicSymbolTable.TryGetValue(Name, out TokenType tokenType))
    //        //{
    //        //    return DicSymbolTable[Name];
    //        //}
    //        throw new InvalidSyntaxException($"Symbol {Name} was not in symbol table");
    //    }
    //    /// <summary>
    //    /// Add symbol to dictionary Symboltable and list of scope Scopetracker
    //    /// </summary>
    //    /// <param Name="Name"></param>
    //    /// <param Name="Type"></param>
    //    public void AddSym(string Name, TokenType Type)
    //    {

    //        if (LookUp(Name, CurrentLevelProp) == false && Symboltable.Any(x=>x.ContainsKey(Name))==false)
    //            {
    //            //add new symboltable
    //            Symboltable.Add(new Dictionary<string, TokenType>());
    //            Symboltable[Symboltable.Count - 1].Add(Name, Type);
    //            //Add Name, level, open, close param
    //           // ScopeTracker.Add(Name, Tuple.Create(CurrentLevelProp,true, false));

    //        }
    //        else
    //        {
    //            //add to current symboltable
    //            Symboltable[Symboltable.Count - 1].Add(Name, Type);
    //        }

    //        throw new InvalidSyntaxException($"Symbol {Name} was not added in symbol table, since it is there already");
    //    }
    //    public bool LookUp(string Name, int Level)
    //    {


    //        if (ScopeTracker.ContainsKey(Name))
    //        {
    //            throw new InvalidSyntaxException($"Symbol { Name } was not added in the symbol table, since it exist already at scope {Level}");
    //        }
    //        return false;
    //    }
    }
}
