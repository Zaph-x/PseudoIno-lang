using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class LeftParenthesisNode : AstNode
    {
        public LeftParenthesisNode(ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}