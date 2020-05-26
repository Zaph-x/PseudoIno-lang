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
    /// <summary>
    /// A builder for the symboltable
    /// </summary>
    public class SymbolTableBuilder
    {
        /// <summary>
        /// The global symbol table
        /// </summary>
        public static SymbolTableObject GlobalSymbolTable;
        /// <summary>
        /// The current symbol table
        /// </summary>
        public SymbolTableObject CurrentSymbolTable;
        /// <summary>
        /// A stack containin each parent scope of the current scope
        /// </summary>
        /// <returns>The current scope</returns>
        public static Stack<SymbolTableObject> TopOfScope = new Stack<SymbolTableObject>();
        /// <summary>
        /// The constructor of the symboltable builder. Here the global scope is set
        /// </summary>
        /// <param name="global">The global scope</param>
        public SymbolTableBuilder(SymbolTableObject global)
        {
            GlobalSymbolTable = global;
            CurrentSymbolTable = global;
        }
        /// <summary>
        /// A method that opens a new scope and symbol table. This symboltable is marked as a child of the current scope
        /// </summary>
        /// <param name="type">The type of scope being opened</param>
        /// <param name="name">The name of the scope being opened</param>
        public void OpenScope(TokenType type, string name)
        {
            CurrentSymbolTable = new SymbolTableObject { Type = type, Name = name, Parent = CurrentSymbolTable };
        }
        /// <summary>
        /// This method will close a scope, and update the current scope
        /// </summary>
        public void CloseScope()
        {
            CurrentSymbolTable = TopOfScope.Pop().Parent;
        }
        /// <summary>
        /// This method will add an AST node to the current scope
        /// </summary>
        /// <param name="node">The node to add to the scope</param>
        public void AddSymbol(AstNode node)
        {
            Symbol symbol = new Symbol(GetNameFromRef(node), node.Type, false, node);
            TopOfScope.Peek().Symbols.Add(symbol);
        }
        /// <summary>
        /// This method will add an array to the current scope
        /// </summary>
        /// <param name="arrNode">The array to add</param>
        public void AddArray(ArrayNode arrNode)
        {
            AddSymbol(arrNode);
            CurrentSymbolTable.DeclaredArrays.Add(arrNode);
        }
        /// <summary>
        /// This method will add a reference to a node
        /// </summary>
        /// <param name="node">The node to add</param>
        public void AddRef(AstNode node)
        {
            Symbol symbol = new Symbol(GetNameFromRef(node), node.Type, true, node);
            TopOfScope.Peek().Symbols.Add(symbol);
        }
        /// <summary>
        /// This method will get the name of a reference
        /// </summary>
        /// <param name="node">The node to get the name from</param>
        /// <returns></returns>
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
