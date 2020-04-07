using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class EndNode : AstNode
    {
        public EndNode()
        {
            this.Type = TokenType.END;
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}