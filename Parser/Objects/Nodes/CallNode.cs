using System;
using Lexer.Objects;
namespace Parser.Objects.Nodes
{
    public class CallNode : AstNode
    {
        public CallNode()
        {
            this.Type = TokenType.CALL;
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}