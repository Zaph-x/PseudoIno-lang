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


        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}