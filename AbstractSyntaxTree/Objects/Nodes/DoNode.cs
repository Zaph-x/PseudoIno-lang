using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class DoNode : AstNode
    {
        public DoNode(ScannerToken token) : base(token)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}