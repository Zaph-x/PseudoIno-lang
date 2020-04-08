using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class LoopFnNode : FuncNode
    {
        public LoopFnNode()
        {
            this.Type = TokenType.LOOP_FN;
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}