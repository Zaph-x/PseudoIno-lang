using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the divide node class
    /// It inherits math operator node
    /// </summary>
    public class DivideNode : MathOperatorNode
    {
        /// <summary>
        /// This is the constructor for divide node
        /// </summary>
        /// <param name="token">This is the token</param>
        public DivideNode(ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}