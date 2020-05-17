using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ExpressionTerm : ExpressionNode
    {
        public ExpressionTerm(ScannerToken token) : base(token)
        {
        }

        public ExpressionTerm(TokenType type, ScannerToken token) : base(type, token)
        {
        }

        public ExpressionTerm(int line, int offset) : base(TokenType.EXPR, line, offset)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}