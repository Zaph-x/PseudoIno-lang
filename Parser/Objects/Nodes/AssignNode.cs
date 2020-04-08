using System;
using Lexer.Objects;
namespace Parser.Objects.Nodes
{
    public class AssignNode : AstNode
    {
        public AssignNode(int line, int offset) : base(TokenType.ASSIGN, line, offset)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}