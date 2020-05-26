using System.Collections.Generic;
using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the else if statement node class
    /// It inherits from statement class and implements the scope interface
    /// </summary>
    public class ElseifStatementNode : StatementNode, IScope
    {
        /// <summary>
        /// This returns the value and sets the value 
        /// </summary>
        public ValNode Val { get; set; }
        /// <summary>
        /// This returns and sets the expression
        /// </summary>
        public ExpressionNode Expression { get; set; }
        /// <summary>
        /// This returns and sets a list of statements
        /// </summary>
        public List<StatementNode> Statements { get; set; }
        /// <summary>
        /// This is the constructor for else if statements
        /// The statements is assigned to a list of statement
        /// </summary>
        /// <param name="line">This is the line of the statment</param>
        /// <param name="offset">This is the offset</param>
        public ElseifStatementNode(int line, int offset) : base(TokenType.ELSEIFSTMNT, line, offset)
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