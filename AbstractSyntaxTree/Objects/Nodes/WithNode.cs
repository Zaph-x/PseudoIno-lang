using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class WithNode : AstNode
    {
        public WithNode(ScannerToken token) : base(token)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}