using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class InNode : AstNode
    {
        public InNode(ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}