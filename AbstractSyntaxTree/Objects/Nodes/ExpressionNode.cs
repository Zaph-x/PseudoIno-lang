using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class ExpressionNode : StatementNode, IAssignment, ITerm, IExpr
    {

        protected ExpressionNode(ScannerToken token) : base(token)
        {
        }

        protected ExpressionNode(TokenType type, ScannerToken token) : base(type, token)
        {
        }

        protected ExpressionNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }

        public ITerm LeftHand { get; set; }
        public OperatorNode Operator { get; set; }
        public IExpr RightHand { get; set; }

        public abstract override void Accept(Visitor visitor);
    }
}