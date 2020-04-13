using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class AssignmentNode : AstNode
    {
        public AssignmentNode(int line, int offset) : base(TokenType.ASSIGNMENT, line, offset)
        {
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}