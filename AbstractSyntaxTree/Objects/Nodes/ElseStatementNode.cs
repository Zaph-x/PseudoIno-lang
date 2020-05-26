using System.Collections.Generic;
using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the else statement node class
    /// Is inherits from statement node class and implements the scope interface
    /// </summary>
    public class ElseStatementNode : StatementNode, IScope
    {
        /// <summary>
        /// This returns and sets the list of statement
        /// </summary>
        public List<StatementNode> Statements { get; set; }
        /// <summary>
        /// This is the constructor for else statements
        /// The statements is assigned to a list of type statement
        /// </summary>
        /// <param name="line">This is the line of statement</param>
        /// <param name="offset">This is the offset</param>
        public ElseStatementNode(int line, int offset) : base(TokenType.ELSE, line, offset)
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