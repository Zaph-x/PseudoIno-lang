using System.Collections.Generic;
using System;
using Lexer.Objects;

namespace Parser.Objects.Nodes
{
    public class FunctionLoopNode : FuncNode
    {
        public List<StatementNode> Statements = new List<StatementNode>();

        public FunctionLoopNode(int line, int offset) : base(TokenType.LOOP_FN, line, offset)
        {
        }


        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}