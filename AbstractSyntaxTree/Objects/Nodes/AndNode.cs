using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// The class for And node
    /// inherits math operator node
    /// </summary>
    public class AndNode : MathOperatorNode
    {
        /// <summary>
        /// The constructor for And node
        /// </summary>
        /// <param name="token">The name of the token</param>
        public AndNode (ScannerToken token) : base(token)
        {
        }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}