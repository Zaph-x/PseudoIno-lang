using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public abstract class ExpressionNode : AstNode
    {
        public Type type { get; set; }

        public ExpressionNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
        }

        public override abstract void Accept(Visitor visitor);
    }
}