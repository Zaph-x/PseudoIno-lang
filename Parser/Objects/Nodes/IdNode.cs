using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class IdNode : AstNode
    {
        public IdNode(int line, int offset) : base (TokenType.)
        {}

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}