using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class FunctionDefinitonNode : FuncNode
    {
        public FunctionDefinitonNode()
        {
            this.Type = TokenType.FUNCTION;
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}