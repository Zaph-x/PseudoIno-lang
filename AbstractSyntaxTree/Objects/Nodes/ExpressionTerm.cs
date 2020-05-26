using Lexer.Objects;
/// <summary>
/// This namespace gives access to node objects in the AST
/// </summary>
namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the expression term node
    /// It inherits from the expression node class
    /// </summary>
    public class ExpressionTerm : ExpressionNode
    {
        /// <summary>
        /// This is the constructor for expression term node
        /// </summary>
        /// <param name="token">This is the token</param>
        public ExpressionTerm(ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}