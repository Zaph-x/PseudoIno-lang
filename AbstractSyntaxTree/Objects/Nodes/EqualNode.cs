using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class EqualNode : BoolOperatorNode
    {
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