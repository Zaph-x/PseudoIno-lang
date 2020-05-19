using System;
using System.Collections.Generic;
using Lexer.Objects;
namespace AbstractSyntaxTree.Objects.Nodes
{
    public class CallNode : StatementNode, IAssignment, IExpr
    {
        public VarNode Id { get; set; }
        public List<ValNode> Parameters { get; set; } = new List<ValNode>();
        public ITerm LeftHand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public OperatorNode Operator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IExpr RightHand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CallNode(int line, int offset) : base(TokenType.CALL, line, offset)
        {
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}