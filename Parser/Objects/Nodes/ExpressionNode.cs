using System;
namespace Parser.Objects.Nodes
{
    public abstract class ExpressionNode : AstNode
    {
        public Type type { get; set; }

        public ExpressionNode()
        { }

        public override abstract void Accept(Visitor visitor);
    }
}