using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class FunctionDefinitonNode : FuncNode
    {
        public FunctionDefinitonNode(int line, int offset) : base(TokenType.FUNCTION, line, offset)
        {
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}