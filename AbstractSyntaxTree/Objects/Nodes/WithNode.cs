using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class WithNode : AstNode
    {
        public WithNode(ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}