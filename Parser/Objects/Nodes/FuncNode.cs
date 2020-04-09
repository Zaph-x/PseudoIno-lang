using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public abstract class FuncNode : AstNode
    {
        public FuncNode()
        {
            this.Type = TokenType.FUNC;
        }

        public abstract override void Accept(Visitor visitor);
    }
}