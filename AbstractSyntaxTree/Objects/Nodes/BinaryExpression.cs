using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class BinaryExpression : ExpressionNode, IExpr
    {
        public ITerm LeftHand { get; set; }
        public new OperatorNode Operator { get; set; }
        public IExpr RightHand { get; set; }
        public BinaryExpression(ScannerToken token) : base(token)
        {
        }

        public BinaryExpression(TokenType type, ScannerToken token) : base(type, token)
        {
        }

        public BinaryExpression(int line, int offset) : base(TokenType.EXPR, line, offset)
        {
        }

        public override string ToString() => $"{LeftHand} {Operator} {RightHand}";

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}