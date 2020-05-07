using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ParenthesisExpression : ExpressionNode, IExpr
    {
        public ITerm LeftHand { get; set; }
        public OperatorNode Operator { get; set; }
        public IExpr RightHand { get; set; }
        public ParenthesisExpression(ScannerToken token) : base(token)
        {
        }

        public ParenthesisExpression(TokenType type, ScannerToken token) : base(type, token)
        {
        }

        public ParenthesisExpression(int line, int offset) : base(TokenType.EXPR, line, offset)
        {
        }

        public override string ToString() => $"({LeftHand} {Operator} {RightHand})";

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}