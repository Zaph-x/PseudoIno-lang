using System.Collections.Generic;
using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class FunctionLoopNode : FuncNode
    {
        public List<StatementNode> Statements = new List<StatementNode>();

        public FunctionLoopNode()
        {
            this.Type = TokenType.LOOP_FN;
        }


        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}