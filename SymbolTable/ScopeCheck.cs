using System;
using Lexer.Objects;

namespace SymbolTable
{
    public class ScopeCheck
    {
        public SymbolTableBuilder SymbolTableBuilder;

        public ScopeCheck(SymbolTableBuilder symbolTableBuilder)
        {
            SymbolTableBuilder = symbolTableBuilder;
            InitCheckScope();
        }

        public void InitCheckScope()
        {
            int depth = SymbolTableBuilder.FinalSymbolTable.Count - 1;
            for (int i = depth; i > 0; i--)
            {
                foreach (var symbolTable in SymbolTableBuilder.FinalSymbolTable[i])
                {
                    foreach (var symbol in symbolTable.Symbols)
                    {
                        if (symbol.IsRef == true)
                        {
                            if (!CheckScope(symbol,symbolTable,i))
                            {
                                throw new Exception("Scope error");
                            }
                            break;
                        }
                    }
                }
            }
        }

        public bool CheckScope(Symbol symbol, SymbolTableObject symbolTable, int currentDepth)
        {
            int NumberOfSymbols = symbolTable.Symbols.Count - 1;
            int i = 0;
            foreach (var symbolInTable in symbolTable.Symbols)
            {
                if (CheckLocalScope(symbol,symbolTable))
                {
                    return true;
                }
                if (CheckFuncs(symbol, symbolTable))
                {
                    
                }
                if (currentDepth == 0 && i == NumberOfSymbols)
                {
                    return false;
                }
                if (i == NumberOfSymbols)
                {
                    return CheckScope(symbol,symbolTable.Parent,currentDepth);
                }

                i++;
            }

            return false;
        }

        public bool CheckFuncs(Symbol symbol, SymbolTableObject symbolTableObject)
        {
            foreach (var child in SymbolTableBuilder.SymbolTables)
            {
                if (child.Name == symbol.Name)
                {
                    return true;
                }
            }
            
            return false;
        }

        public bool CheckLocalScope(Symbol symbol, SymbolTableObject symbolTable)
        {
            foreach (var tableSymbol in symbolTable.Symbols)
            {
                if (tableSymbol.IsRef && tableSymbol.TokenType == TokenType.CALL)
                {
                    if (CheckFuncs(tableSymbol,symbolTable))
                    {
                        return true;
                    }
                }
                else
                {
                    if (tableSymbol.Name == symbol.Name && tableSymbol.AstNode.Line < symbol.AstNode.Line)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}