using System.Collections.Generic;
using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the program node class
    /// It inherits the Ast node class and implements the scope interface
    /// </summary>
    public class ProgramNode : AstNode, IScope
    {
        /// <summary>
        /// This sets and returns a list of statements of the type statementnode
        /// </summary>
        public List<StatementNode> Statements { get; set; }
        /// <summary>
        /// This sets and returns a list of function definitions with the type funcnode
        /// </summary>
        public List<FuncNode> FunctionDefinitons = new List<FuncNode>();
        /// <summary>
        /// This is the loopfunction
        /// </summary>
        public FuncNode LoopFunction;
        /// <summary>
        /// This is the constructor for program node
        /// Statements is assigned to a list of statement nodes
        /// </summary>
        /// <param name="line">This is the line</param>
        /// <param name="offset">This is the offset</param>
        public ProgramNode(int line, int offset) : base(TokenType.PROG, line, offset)
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