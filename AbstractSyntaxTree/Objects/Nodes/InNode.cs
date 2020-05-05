using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class InNode : AstNode
    {
        public InNode(ScannerToken token) : base(token)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}