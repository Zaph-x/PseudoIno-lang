using System;
using Lexer.Objects;
namespace AbstractSyntaxTree.Objects.Nodes
{
    public class BeginNode : StatementNode
    {
        public LoopNode LoopNode { get; set; }
        public BeginNode(int line, int offset) : base(TokenType.BEGIN, line, offset)
        {
        }

        public override void Accept(Visitor visitor) 
        {
            visitor.Visit(this);
        }
    }
}