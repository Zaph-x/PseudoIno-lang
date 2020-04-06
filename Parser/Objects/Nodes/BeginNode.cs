using System;
namespace Parser.Objects.Nodes
{
    public class BeginNode : AstNode
    {
        public BeginNode()
        {}

        public override void Accept(Visitor visitor) 
        {
            visitor.Visit(this);
        }
    }
}