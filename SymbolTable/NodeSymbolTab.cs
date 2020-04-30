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
    public class NodeSymbolTab
    {
        /// <summary>
        /// Children list 
        /// </summary>
        public List<NodeSymbolTab> ChildrenList = new List<NodeSymbolTab>();
        public List<AstNode> Symbols = new List<AstNode>();

        /// <summary>
        /// Parent property
        /// </summary>
        public NodeSymbolTab Parent { get; set; }
        
        //Id
        public Guid Id = Guid.NewGuid();
        
        //Name and Type for the node
        public string Name,Value;
        public TokenType Type;

        /// <summary>
        /// Line og offset for every token
        /// </summary>
        public long Line;
        public int Offset;

        /// <summary>
        /// Constructor to add Name,Type, Line and offset attributes for children.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="token"></param>
        public NodeSymbolTab(AstNode token)
        {
            this.Value = token.Value;
            this.Type = token.Type;
            //this.Name = name;
            this.Line = token.Line;
            this.Offset = token.Offset;
           
        }
        /// <summary>
        /// constructer overloading for use 
        /// </summary>
        public NodeSymbolTab()
        {
       
        }
        //list af tokentype ind 
        //list af scanner token ud

        /// <summary>
        /// Add child to Childrenlist. Set parent property of the child node. Input parameters are Name and Type of the node.
        /// </summary>
        /// <param Name="token"></param>
        public void AddNode(AstNode token, NodeSymbolTab symbolTable)
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

        public void AddBlock(AstNode node, NodeSymbolTab symbolTab)
        {
            if (node.Type == TokenType.FUNC)
            {
                throw new Exception("No functions in functions please!");
            }

            ChildrenList.Add(new NodeSymbolTab(node) { Parent = symbolTab});
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
        
        /*public void AddStatementNode(StatementNode statementNode)
        {
            string name = "";
            AstNode astNode = new AndNode(1,1);
            if (statementNode.Type == TokenType.ASSIGNMENT)
            {
                name = ((AssignmentNode) statementNode).Var.Id;
                astNode = (AstNode) statementNode;
            }
            else if (statementNode.Type == TokenType.CALL)
            {
                //name = ((CallNode) statementNode).Var.Id;
                astNode = (AstNode) statementNode;
            }

            if (astNode.Type == TokenType.OP_AND)
            {
                throw new Exception("nope");
            }
            
            if (!ChildrenList.Any(x => x.Name == name && x.Type == astNode.Type))
            {
                ChildrenList.Add(new NodeSymbolTab(name, astNode) { Parent = this });
            }
            else
            {
                throw new Exception($"Symbol table contains the Name {name}");
            }
        }*/
    }
}

