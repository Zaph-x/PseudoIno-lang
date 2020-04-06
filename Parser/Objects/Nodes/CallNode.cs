using System;
namespace Parser.Objects.Nodes
{
    public class CallNode : AstNode
    {
        public CallNode()
        {}

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}