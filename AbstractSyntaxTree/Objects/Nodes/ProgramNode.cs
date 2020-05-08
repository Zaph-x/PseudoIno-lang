using System.Collections.Generic;
using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ProgramNode : AstNode, IScope
    {
        public List<StatementNode> Statements { get; set; }
        public List<FuncNode> FunctionDefinitons = new List<FuncNode>();
        public FuncNode LoopFunction;
        public ProgramNode(int line, int offset) : base(TokenType.PROG, line, offset)
        {
            this.Statements = new List<StatementNode>();
        }

        public override object Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}