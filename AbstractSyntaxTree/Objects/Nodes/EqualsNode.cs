using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class EqualsNode : AstNode
    {
        public EqualsNode(ScannerToken token) : base(token)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}