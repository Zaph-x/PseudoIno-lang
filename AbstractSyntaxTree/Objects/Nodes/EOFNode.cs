using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class EOFNode : AstNode
    {
        public EOFNode(ScannerToken token) : base(token)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}