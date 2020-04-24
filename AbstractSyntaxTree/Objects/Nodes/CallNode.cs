using System;
using Lexer.Objects;
namespace AbstractSyntaxTree.Objects.Nodes
{
    public class CallNode : StatementNode, IAssginment
    {
        public VarNode VarNode { get; set; }
        public CallParametersNode RightHand { get; set; }
        public CallNode(int line, int offset) : base(TokenType.CALL, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}