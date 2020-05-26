using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the plus node class
    /// It inherits from the math operator node class
    /// </summary>
    public class PlusNode : MathOperatorNode
    {
        /// <summary>
        /// This is the constructor for plus node
        /// </summary>
        /// <param name="token">This is the token</param>
        public PlusNode( ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}