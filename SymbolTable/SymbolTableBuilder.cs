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
    //    //Dictionary for lookup of scope and name to see if there are duplicates. The dictionary also have open and close params. Tuple<level,depth,open, close>
    //    Dictionary<string,Tuple<int,int,bool,bool>> ScopeTracker = new Dictionary<string, Tuple<int,int,bool, bool>>();
    //    //List for symboltable content name and type
    //    List<Dictionary<string, TokenType>> Symboltable = new List<Dictionary<string, TokenType>>(); 
    //    void OpenScope()
    //    {
         
    //    }
    //    void CloseScope(string name)
    //    {
    //        ScopeTracker[name] = Tuple.Create(ScopeTracker[name].Item1, ScopeTracker[name].Item2, ScopeTracker[name].Item3, ScopeTracker[name].Item4==true);
            
    //    }
    //    public TokenType RetrieveSymbol(string name)
    //    {
    //        //if (DicSymbolTable.TryGetValue(name, out TokenType tokenType))
    //        //{
    //        //    return DicSymbolTable[name];
    //        //}
    //        throw new InvalidSyntaxException($"Symbol {name} was not in symbol table");
    //    }
    //    /// <summary>
    //    /// Add symbol to dictionary Symboltable and list of scope Scopetracker
    //    /// </summary>
    //    /// <param name="name"></param>
    //    /// <param name="type"></param>
    //    public void AddSym(string name, TokenType type)
    //    {

    //        if (LookUp(name, CurrentLevelProp) == false && Symboltable.Any(x=>x.ContainsKey(name))==false)
    //            {
    //            //add new symboltable
    //            Symboltable.Add(new Dictionary<string, TokenType>());
    //            Symboltable[Symboltable.Count - 1].Add(name, type);
    //            //Add name, level, open, close param
    //           // ScopeTracker.Add(name, Tuple.Create(CurrentLevelProp,true, false));

    //        }
    //        else
    //        {
    //            //add to current symboltable
    //            Symboltable[Symboltable.Count - 1].Add(name, type);
    //        }

    //        throw new InvalidSyntaxException($"Symbol {name} was not added in symbol table, since it is there already");
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
