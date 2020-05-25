using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class LessNode : MathOperatorNode
    {
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