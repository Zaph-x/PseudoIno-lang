using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class NoParenExpression : ExpressionNode, IExpr
    {
        public ITerm LeftHand { get; set; }
        public OperatorNode Operator { get; set; }
        public IExpr RightHand { get; set; }
        public NoParenExpression(ScannerToken token) : base(token)
        {
        }

        public NoParenExpression(TokenType type, ScannerToken token) : base(type, token)
        {
        }

        public NoParenExpression(int line, int offset) : base(TokenType.EXPR, line, offset)
        {
        }

        public override string ToString() => $"{LeftHand} {Operator} {RightHand}";

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}