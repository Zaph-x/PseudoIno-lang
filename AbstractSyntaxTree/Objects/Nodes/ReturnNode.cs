using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the return node class
    /// It inherits from statement node class
    /// </summary>
    public class ReturnNode : StatementNode
    {
        /// <summary>
        /// This sets and returns the value for retunr value
        /// </summary>
        public ExpressionNode ReturnValue {get;set;}
        /// <summary>
        /// This is the constructor for return node
        /// </summary>
        /// <param name="line">This is the line</param>
        /// <param name="offset">This is the offset</param>
        public ReturnNode(int line, int offset) : base(TokenType.RETURN, line, offset)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}