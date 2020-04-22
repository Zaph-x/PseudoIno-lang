using System;
using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public abstract class FuncNode : StatementNode, IScope
    {
        public FuncNode(TokenType type, int line, int offset) : base(type, line, offset)
        {
            this.Statements = new List<StatementNode>();
        }

        public abstract override void Accept(Visitor visitor);
        public List<StatementNode> Statements { get; set; }
    }
}