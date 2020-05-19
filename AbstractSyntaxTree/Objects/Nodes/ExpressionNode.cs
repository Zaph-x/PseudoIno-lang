using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class ExpressionNode : StatementNode, IAssignment, ITerm, IExpr
    {
        private ExpressionNode _Parent {get;set;}
        public new ExpressionNode Parent {get => _Parent;set {
            this._Parent = value;
            this._Parent.Child = this;
        }}
        public ExpressionNode Child {get;set;}

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
        public new OperatorNode Operator { get; set; }
        public IExpr RightHand { get; set; }

        public abstract override object Accept(Visitor visitor);
    }
}