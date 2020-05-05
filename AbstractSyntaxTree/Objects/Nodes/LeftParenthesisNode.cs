using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class LeftParenthesisNode : AstNode
    {
        public LeftParenthesisNode(ScannerToken token) : base(token)
        {
        }
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}