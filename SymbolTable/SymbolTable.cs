using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Lexer.Objects;
using Parser.Objects;
using System.Linq;
using AbstractSyntaxTree.Objects;
using AbstractSyntaxTree.Objects.Nodes;

namespace SymbolTable
{
    /// <summary>
    /// Symbol table node
    /// </summary>
    public class SymbolTable
    {
        
        public List<AstNode> Symbols = new List<AstNode>();
        
        public SymbolTable Parent { get; set; }
        
        public SymbolTable(AstNode token)
        {

        }
        
        public void AddNode(AstNode token, SymbolTable symbolTable)
        {
            if (TokenTypeExpressions.IsBlock(token.Type))
            {
               AddBlock(token, symbolTable);
            }
            else if (TokenTypeExpressions.IsDcl(token.Type))
            {
               AddDcl(token);
            }
            else if (TokenTypeExpressions.IsRef(token.Type))
            {
               AddRef(token);
            }
        }

        public void AddBlock(AstNode node, SymbolTable symbolTable)
        {
            if (node.Type == TokenType.FUNC)
            {
                throw new Exception("No functions in functions please!");
            }

            ChildrenList.Add(new SymbolTable(node) { Parent = symbolTable});
            //if (this.Parent != null)
            //{
            //    if (Parent.ChildrenList.Any(x => x.Id == this.Id && x.ChildrenList.Count == 0))
            //    {
            //        Parent.ChildrenList.Add(this);
            //    }
            //}
           
        }

        public void AddDcl(AstNode node)
        {
            Symbols.Add(node);
        }

        public void AddRef(AstNode node)
        {
            Findnode(GetNameFromRef(node));
            if (!Parent.Findnode(GetNameFromRef(node)))
            {
                throw new Exception("Symbol not found in symbol table");
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

            return name;
        }
        
        /// <summary>
        /// Findnode methode to recursively find a node. It searches in curent scope , then parents scope , parents parent scope etc.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Findnode(string name)
        {
            return Symbols.Any(child => GetNameFromRef(child) == name);
        }
        
    }
}

