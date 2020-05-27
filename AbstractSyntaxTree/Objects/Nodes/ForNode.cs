using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the class for For loop node
    /// It inherits the statement node class and implement the scope interface
    /// </summary>
    public class ForNode : StatementNode, IScope
    {
        /// <summary>
        /// This sets and returns the counter variable of the for loop
        /// </summary>
        public VarNode CountingVariable { get; set; }
        /// <summary>
        /// This sets and returns the from value of the for loop
        /// </summary>
        public NumericNode From { get; set; }
        /// <summary>
        /// This sets and returns the to value of the for loop
        /// </summary>
        public NumericNode To { get; set; }
        /// <summary>
        /// This sets and returns the list of statments
        /// </summary>
        public List<StatementNode> Statements { get; set; }
        /// <summary>
        /// This is the constructor for a for loop
        /// Statements is assigned to a list of type statementnode 
        /// </summary>
        /// <param name="line">This is the line</param>
        /// <param name="offset">This is the offset</param>
        public ForNode(int line, int offset) : base(TokenType.FOR, line, offset)
        {
            Statements = new List<StatementNode>();
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}