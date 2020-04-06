using System;
namespace Parser.Objects.Nodes
{
    public class FuncNode : AstNode
    {
        public FuncNode()
        {}

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}