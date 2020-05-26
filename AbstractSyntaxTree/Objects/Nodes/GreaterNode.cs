using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    /// <summary>
    /// This is the greater node class
    /// It inherits the math operator node class
    /// </summary>
    public class GreaterNode : MathOperatorNode
    {
        /// <summary>
        /// This is the greater node constructor
        /// </summary>
        /// <param name="token">This is the token </param>
        public GreaterNode( ScannerToken token) : base(token)
        {
        }

        /// <inheritdoc cref="AbstractSyntaxTree.Objects.AstNode.Accept(Visitor)"/>
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}