using System;
namespace Parser.Objects.Nodes
{
    public abstract class StatementNode : AstNode
    {
        public StatementNode()
        {}

        public abstract override void Accept(Visitor visitor);
    }
}