using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public abstract class StatementNode : AstNode
    {
        public StatementNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }

        public abstract override void Accept(Visitor visitor);
    }
}