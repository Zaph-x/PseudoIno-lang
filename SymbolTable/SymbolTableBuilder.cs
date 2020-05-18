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
    public class SymbolTableBuilder
    {
        public static SymbolTableObject GlobalSymbolTable;
        public SymbolTableObject CurrentSymbolTable;
        public static Stack<SymbolTableObject> TopOfScope = new Stack<SymbolTableObject>();
        public SymbolTableBuilder(SymbolTableObject global)
        {
            GlobalSymbolTable = global;
            CurrentSymbolTable = global;
        }

        public void OpenScope(TokenType type, string name)
        {
            CurrentSymbolTable = new SymbolTableObject { Type = type, Name = name, Parent = CurrentSymbolTable };
        }

        public void CloseScope()
        {
            CurrentSymbolTable = TopOfScope.Pop().Parent;
        }

        public void AddSymbol(AstNode node)
        {
            Symbol symbol = new Symbol(GetNameFromRef(node), node.Type, false, node);
            TopOfScope.Peek().Symbols.Add(symbol);
        }

        public void AddRef(AstNode node)
        {
            Symbol symbol = new Symbol(GetNameFromRef(node), node.Type, true, node);
            TopOfScope.Peek().Symbols.Add(symbol);
        }

        public string GetNameFromRef(AstNode node)
        {
            string name = "";
            if (node.Type == TokenType.ASSIGNMENT)
            {
                name = ((VarNode)((AssignmentNode)node).LeftHand).Id;
            }
            else if (node.Type == TokenType.APIN)
            {
                name = ((APinNode)node).Id;
            }
            else if (node.Type == TokenType.DPIN)
            {
                name = ((DPinNode)node).Id;
            }
            else if (node.Type == TokenType.VAR)
            {
                name = ((VarNode)node).Id;
            }
            else if (node.Type == TokenType.CALL)
            {
                name = ((CallNode)node).Id.Id;
            }
            else if (node.Type == TokenType.FUNC)
            {
                name = ((FuncNode)node).Name.Id;
            }
            else if (node.Type == TokenType.VAR)
            {
                name = ((VarNode)node).Id;
            }

            return name;
        }
    }
}
