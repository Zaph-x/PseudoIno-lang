using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the class for less node
    /// It inherits the math operator node
    /// </summary>
    public class LessNode : MathOperatorNode
    {
        /// <summary>
        /// This is the construcor for less node
        /// </summary>
        /// <param name="token">This is the token</param>
        public LessNode( ScannerToken token) : base(token)
        {
        }
        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}