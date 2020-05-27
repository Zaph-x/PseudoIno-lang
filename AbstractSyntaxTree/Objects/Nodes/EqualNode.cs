using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the equal node class
    /// It inherits the bool operator node class
    /// </summary>
    public class EqualNode : BoolOperatorNode
    {
        /// <summary>
        /// This is the constructor for equal node
        /// </summary>
        /// <param name="token">This is the token</param>
        public EqualNode( ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}