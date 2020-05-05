using System;

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
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }

        public bool CheckScope(Symbol symbol, SymbolTable symbolTable, int currentDepth)
        {
            int NumberOfSymbols = symbolTable.Symbols.Count - 1;
            int i = 0;
            foreach (var symbolInTable in symbolTable.Symbols)
            {
                if (symbolInTable.Name == symbol.Name && 
                    symbolInTable.IsRef == false && 
                    symbol.AstNode.Line < symbolInTable.AstNode.Line)
                {
                    return true;
                }
                if (currentDepth == 0 && i == NumberOfSymbols)
                {
                    return false;
                }
                if (i == NumberOfSymbols)
                {
                    return CheckScope(symbol,symbolTable.Parent,currentDepth-1);                    
                }
            }

            return false;
        }
    }
}