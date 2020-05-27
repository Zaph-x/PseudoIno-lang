using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the whilenode class
    /// It inherits the statementnode and implements the scope interface
    /// </summary>
    public class WhileNode : StatementNode, IScope
    {
        /// <summary>
        /// This sets and returns the value of the expression
        /// </summary>
        public ExpressionNode Expression { get; set; }
        /// <summary>
        /// This sets and returns the value of the list of statements
        /// </summary>
        public List<StatementNode> Statements { get; set; }
        /// <summary>
        /// This is the while node constructor
        /// Statements are assigned to a list of statementnodes
        /// </summary>
        /// <param name="line">This is the line</param>
        /// <param name="offset">This is the offset</param>
        public WhileNode(int line, int offset) : base(TokenType.WHILE, line, offset)
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