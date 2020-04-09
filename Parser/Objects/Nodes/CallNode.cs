using System;
using Lexer.Objects;
namespace Parser.Objects.Nodes
{
    public class CallNode : AstNode
    {
        public CallNode(int line, int offset) : base(TokenType.CALL, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}