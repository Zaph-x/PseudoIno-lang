using System.Collections.Generic;
using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class ProgramNode : AstNode
    {
        public List<StatementNode> Statements = new List<StatementNode>();
        public List<FunctionDefinitonNode> FunctionDefinitons = new List<FunctionDefinitonNode>();
        public FunctionLoopNode LoopFunction;
        public ProgramNode(int line, int offset) : base(TokenType.PROG, line, offset)
        {
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}