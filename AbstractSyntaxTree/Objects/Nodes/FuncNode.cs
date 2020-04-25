using System;
using System.Collections.Generic;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class FuncNode : StatementNode, IScope
    {
        public List<StatementNode> Statements { get; set; }
        public VarNode Name { get; set; }
        public List<VarNode> FunctionParameters {get;set;} = new List<VarNode>();

        public FuncNode(int line, int offset) : base(TokenType.FUNC, line, offset)
        {
            this.Statements = new List<StatementNode>();
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}