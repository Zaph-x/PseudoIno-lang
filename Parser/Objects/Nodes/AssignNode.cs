using System;
namespace Parser.Objects.Nodes
{
    public class AssugnNode : AstNode
    {
        public AssugnNode()
        {}

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}