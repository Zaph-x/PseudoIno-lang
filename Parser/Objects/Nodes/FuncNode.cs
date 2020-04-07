using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class FuncNode : StatementNode
    {
        public FuncNode()
        {
            this.Type = TokenType.FUNC;
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}