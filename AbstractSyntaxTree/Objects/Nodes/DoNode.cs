using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class DoNode : AstNode
    {
        public DoNode(ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}