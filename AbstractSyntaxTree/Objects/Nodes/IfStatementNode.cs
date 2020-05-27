using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the if statement node class
    /// It inherits a statement class and implements the scope interface
    /// </summary>
    public class IfStatementNode : StatementNode , IScope
    {
        //private node condition { get; set; }
        /// <summary>
        /// This sets and returns the value of expression node
        /// </summary>
        public ExpressionNode Expression { get; set; }
        /// <summary>
        /// This sets and returns the value of the list of expression node
        /// </summary>
        public List<StatementNode> Statements { get; set; }
        /// <summary>
        /// This is the constructor for if statements
        /// Statements is assigned to a list of statement
        /// </summary>
        /// <param name="line">This is the line of the statment</param>
        /// <param name="offset">This is the offset of the statement</param>
        public IfStatementNode(int line, int offset) : base(TokenType.IFSTMNT, line, offset)
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