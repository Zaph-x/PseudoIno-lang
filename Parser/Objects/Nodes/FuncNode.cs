using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public abstract class FuncNode : AstNode
    {
        public FuncNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }

        public abstract override void Accept(Visitor visitor);
    }
}