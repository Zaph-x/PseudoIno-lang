using System.Collections.Generic;
using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class FunctionLoopNode : FuncNode
    {
        public List<StatementNode> Statements = new List<StatementNode>();

        public FunctionLoopNode(int line, int offset) : base(line, offset)
        {
        }


        public override object Accept(Visitor visitor) {
            return visitor.Visit(this);
        }
    }
}