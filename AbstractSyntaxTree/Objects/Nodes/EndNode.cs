using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public class EndNode : AstNode
    {
        public EndNode(ScannerToken token) : base(token)
        {
        }

        public override object Accept(Visitor visitor) {
            return visitor.Visit(this);
        }
    }
}