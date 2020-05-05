using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class NewlineNode : AstNode
    {
        public NewlineNode(ScannerToken token) : base(token)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}