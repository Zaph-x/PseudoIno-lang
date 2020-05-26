using System;
using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the class for func node
    /// It inherits the statement node class and implement the scope interface
    /// </summary>
    public class FuncNode : StatementNode, IScope
    {
        /// <summary>
        /// This sets and returns a list of statement nodes
        /// </summary>
        public List<StatementNode> Statements { get; set; }
        /// <summary>
        /// This sets and returns the name of var node
        /// </summary>
        public VarNode Name { get; set; }
        /// <summary>
        /// This sets and returns the function parameters with the type var node
        /// </summary>
        public List<VarNode> FunctionParameters {get;set;} = new List<VarNode>();
        /// <summary>
        /// This is the constructor for func node
        /// Statement is assigned to a list of statementnodes
        /// </summary>
        /// <param name="line"></param>
        /// <param name="offset"></param>
        public FuncNode(int line, int offset) : base(TokenType.FUNC, line, offset)
        {
            this.Statements = new List<StatementNode>();
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}