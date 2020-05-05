using System;
using Lexer.Objects;
namespace AbstractSyntaxTree.Objects.Nodes
{
    public class BeginNode : StatementNode
    {
        public LoopNode LoopNode { get; set; }
        public BeginNode(ScannerToken token) : base(token)
        {
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}