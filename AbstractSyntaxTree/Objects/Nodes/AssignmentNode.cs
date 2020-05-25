using System.Linq.Expressions;
using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class AssignmentNode : StatementNode, IExpr
    {
        public IExpr RightHand { get; set; }
        public ITerm LeftHand { get; set; }
        public OperatorNode Operator { get; set; }
        public AssignmentNode(int line, int offset) : base(TokenType.ASSIGNMENT, line, offset)
        {
        }
        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}