using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class NewlineNode : AstNode
    {
        public NewlineNode(ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}