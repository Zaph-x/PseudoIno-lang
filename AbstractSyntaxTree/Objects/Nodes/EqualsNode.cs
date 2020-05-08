using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class EqualsNode : AstNode
    {
        public EqualsNode(ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}