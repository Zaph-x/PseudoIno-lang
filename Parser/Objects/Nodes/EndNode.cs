using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class EndNode : AstNode
    {
        public EndNode(int line, int offset) : base (TokenType.END, line, offset)
        {
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}