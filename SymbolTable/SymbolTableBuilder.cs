using System.Collections.Generic;
using Lexer.Exceptions;
using Lexer.Objects;
using System.Collections;
using System;
using System.Linq;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;

namespace SymbolTable
{
    class SymbolTableBuilder
    {
        public List<SymbolTable> SymbolTables = new List<SymbolTable>();
        public List<List<SymbolTable>> FinalSymbolTable = new List<List<SymbolTable>>();
        public SymbolTable GlobalSymbolTable;
        public SymbolTable CurrentSymbolTable;
        public Stack<SymbolTable> TopOfScope = new Stack<SymbolTable>();
        public int Depth { get; set; }
        public SymbolTableBuilder(SymbolTable global)
        {
            GlobalSymbolTable = global;
            CurrentSymbolTable = global;
            //SymbolTables.Add(new List<SymbolTable>());
            //SymbolTables[0][0] = new SymbolTable(GlobalSymbolTable);
        }

        public void OpenScope(TokenType type, string name)
        {
            Depth++;
            /*if (SymbolTables.Count < Depth + 1)
            {
                SymbolTables.Add(new List<SymbolTable>());
            }*/
            SymbolTable symbolTable;
            if (Depth == 1)
            {
                symbolTable = new SymbolTable {Type = type, Name = name, Depth = Depth};
            }
            else
            {
                symbolTable = new SymbolTable {Type = type, Name = name, Depth = Depth, Parent = TopOfScope.Peek()};                
            }
            TopOfScope.Push(symbolTable);
        }
        
        public void CloseScope()
        {
            Depth--;
            SymbolTables.Add(TopOfScope.Peek());
            TopOfScope.Pop();
        }

        public void AddSymbol(AstNode node)
        {
            Symbol symbol = new Symbol(GetNameFromRef(node),node.Type, false,node);
            TopOfScope.Peek().Symbols.Add(symbol);
        }

        public void AddRef(AstNode node)
        {
            Symbol symbol = new Symbol(GetNameFromRef(node),node.Type, true,node);
            TopOfScope.Peek().Symbols.Add(symbol);
        }

        public void MakeFinalTable()
        {
            int maxDetph = 0;
            foreach (var symbolTable in SymbolTables)
            {
                if (symbolTable.Depth > maxDetph)
                {
                    maxDetph = symbolTable.Depth;
                }
            }
            for (int i = 0; i < maxDetph; i++)
            {
                FinalSymbolTable.Add(new List<SymbolTable>());
            }
            foreach (var symbolTable in SymbolTables)
            {
                FinalSymbolTable[symbolTable.Depth-1].Add(symbolTable);
            }
        }

        public string GetNameFromRef(AstNode node)
        {
            string name = "";
            if (node.Type == TokenType.ASSIGNMENT)
            {
                name = ((AssignmentNode) node).Var.Id;
            }
            else if (node.Type == TokenType.APIN)
            {
                name = ((APinNode) node).Id;
            }
            else if (node.Type == TokenType.DPIN)
            {
                name = ((DPinNode) node).Id;
            }
            else if (node.Type == TokenType.VAR)
            {
                name = ((VarNode) node).Id;
            }
            else if (node.Type == TokenType.CALL)
            {
                name = ((CallNode) node).Id.Id;
            }
            else if (node.Type == TokenType.FUNC)
            {
                name = ((FuncNode) node).Name.Id;
            }
            else if (node.Type == TokenType.VAR)
            {
                name = ((VarNode) node).Id;
            }

            return name;
        }
        
        public bool Findnode(string name)
        {
            return CurrentSymbolTable.Symbols.Any(child => child.Name == name);
        }
    }
}
