using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ExpressionTerm : ExpressionNode
    {
        public ExpressionTerm(ScannerToken token) : base(token)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}