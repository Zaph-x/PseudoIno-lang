using System;
namespace Parser.Objects.Nodes
{
    public class EndNode : AstNode
    {
        public EndNode()
        {}

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}